using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;

namespace LTMCB_GK_Backend {
    class Result {
        //state:-1 for failure  | positive int for index of socket used
        public int State { get; set; }
        public String Message { get; set; }

        public Result(int state, String m) {
            this.State = state;
            this.Message = m;
        }

        public override String ToString() {
            return this.Message;
        }
    }


    class ServiceModel {
        private TcpServerModel tcp;
        private SocketModel[] socketArr;
        private int[] socketStateArr;
        private UserModel[] userArr;
        private Database DB = new Database("mongodb://localhost:27017", "LTMCB_DB");
        static String IP = "127.0.0.1";
        static int Port = 13000;
        static int MAX_NUMBER_CLIENTS = 2;
        static double Mpr = 0.1;

        public ServiceModel() {
            this.tcp = new TcpServerModel(IP, Port);
            this.socketArr = new SocketModel[MAX_NUMBER_CLIENTS];
            this.socketStateArr = new int[MAX_NUMBER_CLIENTS];
            this.userArr = new UserModel[MAX_NUMBER_CLIENTS];
        }
        public ServiceModel(String ip, int p) {
            this.tcp = new TcpServerModel(ip, p);
            this.socketArr = new SocketModel[MAX_NUMBER_CLIENTS];
            this.socketStateArr = new int[MAX_NUMBER_CLIENTS];
            this.userArr = new UserModel[MAX_NUMBER_CLIENTS];
        }

        public int GetFreeSocket() {
            for( int i = 0; i < MAX_NUMBER_CLIENTS; i++) {
                if(this.socketStateArr[i] == 0) {
                    return i;
                }
            }

            return -1; //Server overloaded
        }

        public void StartServer() {
            this.tcp.Listen();
        }

        public Result AcceptConnection() {
            Exception tmp = new Exception("Server overloaded!");
            try {
                Socket s = tcp.SetUpANewConnection();

                int freeSocket = this.GetFreeSocket();
                if(freeSocket == -1) {
                    s.Close();
                    throw tmp;
                }

                

                this.socketArr[freeSocket] = new SocketModel(s);
                this.socketStateArr[freeSocket] = 1;

                Console.WriteLine("New connection on socket [" + freeSocket +
                    "] from: " +
                    this.socketArr[freeSocket].GetRemoteEndpoint());

                return new Result(freeSocket, this.socketArr[freeSocket].GetRemoteEndpoint());
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                if (e == tmp)
                    return new Result(-2, e.Message);
                return new Result(-1, e.Message); //Connection fail
            }
        }

        private void ServeSignup(int index, String param) {
            UserModel tmp = new UserModel(this.DB);

            String[] str = param.Split(';');
            if(str.Length == 2) {
                bool state = tmp.AddUser(str[0], str[1]);
                if(state) {
                    this.socketArr[index].SendData("Success:Signed up ok!");
                } else {
                    this.socketArr[index].SendData("Failure:Signed up fail!");
                }

            } else {
                this.socketArr[index].SendData("Failure:Signed up failed!");
            }
        }

        private void ServeSignin(int index, String param) {
            UserModel tmp = new UserModel(this.DB);

            String[] str = param.Split(';');
            if (str.Length == 2) {
                UserModel user = tmp.Authenticate(str[0], str[1]);
                if (user != null) {
                    this.userArr[index] = tmp;
                    this.socketArr[index].SendData("Success:" +
                        user.username + ";" + user.money + ";" + Mpr);
                } else {
                    this.socketArr[index].SendData("Failure:Signed in fail!");
                }

            } else {
                this.socketArr[index].SendData("Failure:Signed in fail!");
            }
        }

        private void ServeCalculation(int index, String param) {
            if(this.userArr[index] == null) {
                this.socketArr[index].SendData("Failure: Please login first!");
                return;
            } else if(this.userArr[index].money - Mpr < 0) {
                this.socketArr[index].SendData("Failure: Please add more money into your account!");
                return;
            }
            
            String[] request = param.Split(';');

            if(request.Length == 2) {
                int n;
                bool isValidNumber = int.TryParse(request[1], out n);

                if(isValidNumber) {
                    if(request[0] == "Iteration") {
                        Stopwatch timer = new Stopwatch();

                        timer.Start();
                        long res = this.SumFactorials(n);
                        timer.Stop();

                        this.userArr[index].UpdateMoney(-0.1);

                        this.socketArr[index].SendData("Result:" +
                            res + ";" + timer.ElapsedMilliseconds + ";" +
                            this.userArr[index].username + ";" +
                            this.userArr[index].money + ";" +
                            Mpr
                            );
                    } else {
                        Stopwatch timer = new Stopwatch();

                        timer.Start();
                        long res = this.SumFactorials2(n);
                        timer.Stop();

                        this.userArr[index].UpdateMoney(-0.1);

                        this.socketArr[index].SendData("Result:" +
                            res + ";" + timer.ElapsedMilliseconds + ";" +
                            this.userArr[index].username + ";" +
                            this.userArr[index].money + ";" +
                            Mpr
                            );
                    }
                }
            } else {
                this.socketArr[index].SendData("Failure:Something went wrong!");
            }
        }

        public void ServeRecharge(int index, String param) {
            try {
                String[] request = param.Split(';');
                double n;
                bool isValidMoney = double.TryParse(request[0], out n);
                if(isValidMoney && 
                   this.userArr[index] != null && 
                   this.userArr[index].isAuthenticated == true) {

                    this.userArr[index].UpdateMoney(n);
                    this.socketArr[index].SendData(
                        "Success:Your money in account is" + this.userArr[index].money);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                this.socketArr[index].SendData("Failure:Something went wrong!");
            }
        }

        public void ServeRequests(Object obj) {
            int index = (int)obj;

            if(index < 0) {
                return;
            }
            
            while(true) {
                String str = this.socketArr[index].ReceiveData();

                if(str == null) {
                    this.socketArr[index].CloseSocket();
                    this.socketStateArr[index] = 0;
                    this.userArr[index] = null;

                    Console.WriteLine("Closed socket[" + index + "]");
                    break;
                }
                String[] request = str.Split(':');

                if(request.Length == 2) {
                    switch (request[0]) {
                        case "Request":
                            ServeCalculation(index,  request[1]);
                            break;
                        case "Signin":
                            ServeSignin(index, request[1]);
                            break;
                        case "Signup":
                            ServeSignup(index, request[1]);
                            break;
                        case "Recharge":
                            ServeRecharge(index, request[1]);
                            break;
                    }
                } else {
                    this.socketArr[index].SendData("Failure:Something went wrong!");
                }
            }
        }

        public void ServeMultiClient(Object obj) {
            ListBox lstConnection = (ListBox)obj;

            while (true) {
                Result res = AcceptConnection();
                if (res.State >= 0) {
                    lstConnection.Items.Add(res.ToString() + " connected");
                } else if (res.State == -1) {
                    lstConnection.Items.Add("Connection failed: " + res.ToString());
                    break;
                } else {
                    lstConnection.Items.Add("Connection failed: " + res.ToString());
                    continue;
                }

                Thread t = new Thread(ServeRequests);
                t.Start(res.State);
            }
        }

        public void StopServer() {
            this.tcp.Shutdown();
        }






        /************* Calculate sum of factorials ************/
        private long CalculateFactorial(int n) {
            if (n < 0) return -1; //n is invalid

            long res = 1;
            for (int i = n; i >= 1; i--) {
                res *= i;
            }

            return res;
        }

        private long SumFactorials(int n) {
            if (n < 1) return -1; //n is invalid

            long res = 0;
            for (int i = n; i >= 1; i--) {
                res += CalculateFactorial(i);
            }

            return res;
        }

        private long CalculateFactorial2(int n) {
            if (n == 0) return 1; //For exception 0! = 1

            return n <= 1 ? n : n * CalculateFactorial2(n - 1);
        }

        private long SumFactorials2(int n) {
            return n <= 1 ? n : CalculateFactorial2(n) + SumFactorials2(n - 1);
        }
        /*********** End calculate sum of factorials **********/
    }
}
