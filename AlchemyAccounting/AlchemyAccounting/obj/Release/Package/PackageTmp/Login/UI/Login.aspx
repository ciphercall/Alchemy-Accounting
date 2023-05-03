<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AlchemyAccounting.Login.UI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="Sergey Pozhilov (GetTemplate.com)" />
    <script src="js/jquery-2.1.4.js"></script>

    <script src="js/bootstrap.min.js"></script>
    <!-- <script src="js/headroom.min.js"></script>
    <script src="js/jQuery.headroom.min.js"></script>
    <script src="js/template.js"></script> -->
    <title>Trade Abroad :: Sign in</title>



    <link rel="stylesheet" media="screen" href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />

    <!-- Custom styles for our template -->
    <link rel="stylesheet" href="css/bootstrap-theme.css" media="screen" />

    <link rel="stylesheet" href="owl-carousel/owl.carousel.css">
    <link rel="stylesheet" href="owl-carousel/owl.theme.css">
    <script src="owl-carousel/owl.carousel.js"></script>
    <script src="js/functions.js"></script>

    <link rel="stylesheet" href="css/main.css" />
    <link rel="stylesheet" href="css/promo-login.css" />
    <script src="js/promo-login.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
        <!-- Fixed navbar -->

        <div class="navbar navbar-inverse navbar-fixed-top headroom">
            <div class="container">
                <div class="navbar-header">
                    <!-- Button for smallest screens -->
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                    <a class="navbar-brand" href="#">Trade Abroad</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav pull-right">

                        <li class="active"><a class="btn" href="LogIn.aspx">SIGN IN / SIGN UP</a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
        <!-- /.navbar -->

        <header id="head" class="secondary"></header>

        <!-- container -->
        <div class="container" id="promo-slider">
            <div id="promo-slider-images"></div>
            <!--  <img src="http:\/\/alchemy-bd.com\/wp-content\/uploads\/clients\/uniontc.png" alt="">
            <img src="http:\/\/alchemy-bd.com\/wp-content\/uploads\/clients\/surgiscope.png" alt=""> -->


            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:TextBox runat="server" Style="display: none" ID="txtIp" ClientIDMode="Static"></asp:TextBox>
                    <div class="panel panel-default" id="login-form">
                        <div class="panel-body">
                            <h3 class="thin text-center">Sign in to your Account</h3>

                            <hr />
                            <asp:TextBox runat="server" Style="display: none" ID="txtLotiLongTude"></asp:TextBox>

                            <div class="top-margin">
                                <label>Username/Email <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtUserName" OnTextChanged="txtUserName_TextChanged" type="text" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="top-margin">
                                <label>Password <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPassword" OnTextChanged="txtPassword_TextChanged" type="password" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="text-center; top-margin">
                                <asp:Label runat="server" ID="lblErrmsg" Visible="False" ForeColor="red"></asp:Label>
                            </div>
                            <asp:TextBox ID="txtlink" ClientIDMode="Static" Style="display: none" class="form-control" runat="server"></asp:TextBox>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <!--<b><a href="#">Forgot password?</a></b>-->
                                </div>
                                <div class="col-lg-6 text-right">
                                    <asp:Button ID="loginButton" runat="server" Text="Sign in" CssClass="btn btn-warning"
                                        OnClick="loginButton_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>

        <footer id="footer">

            <div class="footer1">
                <div class="container">
                    <div class="row">

                        <div class="col-md-4 widget">
                            <h3 class="widget-title">Dhaka</h3>
                            <div class="widget-body">
                                <p>
                                    <!--Tel: <br/>-->
                                    Tel-(+8802)8415473,Fax-(+8802)8415475<br/>
                                    <a href="mailto:#">tradeabroad96dhk@gmail.com</a><br/>
                                    Adept S.T.Complex(3rd Floor), KA-7/1-2,Jagannathpur,<br/>
                                    Bashundhara Link Road,Dhaka-1229,Bangladesh
                                </p>
                            </div>
                        </div>

                        <div class="col-md-4 widget">
                        <h3 class="widget-title">Chittagong</h3>
                            <div class="widget-body">
                                <p>
                                    <!--Tel: <br/>-->
                                    Tel-031 2520108<br/>
                                    <a href="mailto:#">tradeabroad96dhk@gmail.com</a><br/>
                                    Gawsia Bhaban(3rd Floor),156,Sheikh Mojib Road, 
                                    <br/>Agrabad C/A,Chittagong, Bangladesh
                                </p>
                            </div>
                        </div>

						 <div class="col-md-1 widget">
                        </div>

                        <div class="col-md-3 widget">
                            <h3 class="widget-title">Follow me</h3>
                            <div class="widget-body">
                                <p class="follow-me-icons clearfix">
                                    <a href="#"><i class="fa fa-google-plus"></i></a>
                                    <a href="#" target="_blank"><i class="fa fa-facebook"></i></a>
                                </p>

                                <p>
                                    Copyright &copy; <%=DateTime.Now.Year %>, Trade Abroad<br />
                                    Developed by <a href="http://alchemy-bd.com/" target="_blank" rel="designer">Alchemy Software</a>
                                </p>
                            </div>
                        </div>

                    </div>
                    <!-- /row of widgets -->
                </div>
            </div>
        </footer>

        <script type="text/javascript"> 
            $(document).ready(function () {
                navigator.geolocation.getCurrentPosition(showPosition);
                function showPosition(position) {
                    var coordinates = position.coords;
                    var long = coordinates.longitude;
                    var loti = coordinates.latitude;
                    $("#<=txtLotiLongTude.ClientID >").val(loti + ", " + long);

                }
            });
        </script>
    </form>
</body>
</html>
