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
    /// <summary>
    /// Include:
    /// +State: 
    ///     -(> 0): Free socket index
    ///     -(-1) : Server interrupted
    ///     -(-2) : Ser overloaded
    ///     
    /// +Message: Message string
    /// </summary>
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

    /// <summary>
    /// -ConnectionBox: Box to display connection info on form
    /// -FunctionBox: Box to display request info ob form
    /// -Result: Include index and message attacked
    /// </summary>
    class WrapBox {
        public ListBox ConnectionBox { get; set; }
        public ListBox FunctionBox { get; set; }

        public Result Result { get; set; }

        public WrapBox() { }
        public WrapBox(ListBox cb, ListBox fb) {
            this.ConnectionBox = cb;
            this.FunctionBox = fb;
            this.Result = null;
        }
        public WrapBox(ListBox cb, ListBox fb, Result res) {
            this.ConnectionBox = cb;
            this.FunctionBox = fb;
            this.Result = res;
        }
        public WrapBox(ListBox cb, ListBox fb, int state, String mess) {
            this.ConnectionBox = cb;
            this.FunctionBox = fb;
            this.Result = new Result(state, mess);
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
        static double Mpr = 0.1; //cost per a request

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

        private bool isSignedin(UserModel u) {
            foreach(UserModel user in this.userArr) {
                if(u != null && user != null && u.username == user.username) {
                    return true;
                }
            }

            return false;
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
                    return new Result(-2, e.Message); //Server overloaded
                return new Result(-1, e.Message); //Connection fail
            }
        }

        public void ServeSignup(int index, String reqStr, WrapBox wb) {
            UserModel tmp = new UserModel(this.DB);

            String[] str = reqStr.Split(';');
            if(str.Length == 2) {
                bool state = tmp.AddUser(str[0], str[1]);
                if(state) {
                    this.socketArr[index].SendData("Success:Signed up successfully!");

                    wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                       ": request sign up ok");
                } else {
                    this.socketArr[index].SendData("Failure:Signed up failed!");

                    wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                       ": request sign up failed");
                }
            } else {
                this.socketArr[index].SendData("Failure:Signed up failed!");

                wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                       ": request sign up failed");
            }
        }

        public void ServeSignin(int index, String reqStr, WrapBox wb) {
            UserModel tmp = new UserModel(this.DB);

            String[] str = reqStr.Split(';');
            if (str.Length == 2) {
                UserModel user = tmp.Authenticate(str[0], str[1]);
                if (user != null && !this.isSignedin(user)) {
                    this.userArr[index] = user;
                    this.socketArr[index].SendData("Success:" +
                        user.username + ";" + user.money + ";" + Mpr);

                    wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                        ": request sign in ok");
                } else {
                    this.socketArr[index].SendData("Failure:Signed in failed!");

                    wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                        ": request sign in failed");
                }

            } else {
                this.socketArr[index].SendData("Failure:Signed in failed!");

                wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                       ": request sign up failed");
            }
        }

        private void CalculateByLoop(int n, int index) {
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
        }

        private void CalculateByRecursion(int n, int index) {
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

        public void ServeCalculation(int index, String reqStr, WrapBox wb) {
            if(this.userArr[index] == null) {
                this.socketArr[index].SendData("Failure:Please login first!");
                return;
            } else if(this.userArr[index].money - Mpr < 0) {
                this.socketArr[index].SendData("Failure:The amount remaining in your account is not enough to fulfil this request!");
                return;
            }
            
            String[] request = reqStr.Split(';');

            if(request.Length == 2) {
                int n;
                bool isValidNumber = int.TryParse(request[1], out n);

                if(isValidNumber) {
                    if(request[0] == "Iteration") {
                        this.CalculateByLoop(n, index);

                        wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                            ": request calculation by looping");
                    } else {
                        this.CalculateByRecursion(n, index);

                        wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                            ": request calculation by recursing");
                    }
                }
            } else {
                this.socketArr[index].SendData("Failure:Something went wrong!");

                wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                            ": request calculation failed");
            }
        }

        public void ServeRecharge(int index, String reqStr, WrapBox wb) {
            try {
                String[] request = reqStr.Split(';');

                double n;
                bool isValidMoney = double.TryParse(request[0], out n);
                if(isValidMoney && 
                   this.userArr[index] != null && 
                   this.userArr[index].isAuthenticated) {

                    this.userArr[index].UpdateMoney(n);
                    this.socketArr[index].SendData(
                        "Success:Your money in account are " + this.userArr[index].money);

                    wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                        ": Added " + n + " into account");
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                this.socketArr[index].SendData("Failure:Something went wrong when trying to recharge!!");

                wb.FunctionBox.Items.Add(this.socketArr[index].GetRemoteEndpoint() +
                    ": Could not recharge");
            }
        }

        public void ServeRequests(Object obj) {
            WrapBox wb = (WrapBox)obj;

            int index = wb.Result.State;

            if(index < 0) { // Server overloaded or server is stoped suddenly
                return;
            }
            
            while(true) {
                String str = this.socketArr[index].ReceiveData();

                //socket closed
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
                            this.ServeCalculation(index,  request[1], wb);
                            break;
                        case "Signin":
                            this.ServeSignin(index, request[1], wb);
                            break;
                        case "Signup":
                            this.ServeSignup(index, request[1], wb);
                            break;
                        case "Recharge":
                            this.ServeRecharge(index, request[1], wb);
                            break;
                    }
                } else {
                    this.socketArr[index].SendData("Failure:Something went wrong!");
                }
            }
        }

        public void ServeMultiClient(Object obj) {
            WrapBox wb = (WrapBox)obj;

            while(true) {
                wb.Result = AcceptConnection();

                Result res = wb.Result;
                if (res.State >= 0) {
                    wb.ConnectionBox.Items.Add(res.ToString() + " connected");
                } else if (res.State == -1) {
                    wb.ConnectionBox.Items.Add("Connection failed: " + res.ToString());
                    break;
                } else {
                    wb.ConnectionBox.Items.Add("Connection failed: " + res.ToString());
                    continue;
                }

                Thread t = new Thread(ServeRequests);
                t.Start(wb);
            }
        }

        public void StopServer() {
            this.tcp.Shutdown();
        }






        /************* Calculate sum of factorials ************/
        //Calculate factorials by looping
        private long CalculateFactorial(int n) {
            if (n < 0) return -1; //n is invalid

            long res = 1;
            for (int i = n; i >= 1; i--) {
                res *= i;
            }

            return res;
        }

        //Caculate sum of factorials by looping
        private long SumFactorials(int n) {
            if (n < 1) return -1; //n is invalid

            long res = 0;
            for (int i = n; i >= 1; i--) {
                res += CalculateFactorial(i);
            }

            return res;
        }

        //Calculate factorials by recursing
        private long CalculateFactorial2(int n) {
            if (n == 0) return 1; //For exception 0! = 1

            return n <= 1 ? n : n * CalculateFactorial2(n - 1);
        }

        //Calculate sum of factorials by recursing
        private long SumFactorials2(int n) {
            return n <= 1 ? n : CalculateFactorial2(n) + SumFactorials2(n - 1);
        }
        /*********** End calculate sum of factorials **********/
    }
}
