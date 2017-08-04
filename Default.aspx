<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Ready 2017 Demo</title>
    <style>
        body { font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: small; /*background-color: #eeeeee;*/ }
        .title { color: #2196f3; font-size: 20px; font-weight: bold; letter-spacing: -1px; line-height: 1; vertical-align: middle; }
        .border { padding: 5px; border-bottom: 1px solid #2196f3; }
        .bottomleft { position: absolute; bottom: 8px; right: 16px; }
        .button { padding: 5px 15px; font-size: 13px; text-align: center; cursor: pointer; outline: none; color: #fff; background-color: #2196f3; border: none; border-radius: 9px; box-shadow: 0 5px #999; }
        .button:hover { background-color: #3e8e41 }
        .button:active { background-color: #3e8e41; box-shadow: 0 5px #666; transform: translateY(1px); }

        .field-textarea { height: 100px; }
        .form1 textarea { box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; border:1px solid #BEBEBE; padding: 7px; margin:0px; -webkit-transition: all 0.30s ease-in-out;
            -moz-transition: all 0.30s ease-in-out; -ms-transition: all 0.30s ease-in-out; -o-transition: all 0.30s ease-in-out; outline: none; }
        .form1 textarea:focus { -moz-box-shadow: 0 0 8px #88D5E9; -webkit-box-shadow: 0 0 8px #88D5E9; box-shadow: 0 0 8px #88D5E9; border: 1px solid #88D5E9; }
    </style>

    <!--alerts & spinner-->
    <style>
        .alert { padding: 10px; padding-top: 0px; color: white; width: 200px; font-size: 10px !important; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .closebtn { margin-left: 15px; color: white; float: right; font-size: 18px; line-height: 20px; cursor: pointer; transition: 0.3s; }
        .closebtn:hover { font-weight: bold; }

        .modal { position: fixed; top: 0; left: 0; background-color: black; z-index: 100000000; opacity: 0.8; filter: alpha(opacity=80); -moz-opacity: 0.8; min-height: 100%; width: 100%; } 
        .loading { width: 200px; height: 100px; display: none; position: fixed; z-index: 100000001; }
    </style>


    <!--CSS for mobile resolution-->
    <style type="text/css">
        .leftSideBar{display:none !important;}
        .rightSideBar{display:none !important;}

        /* code that is here, until the first @media block, will apply to any screen size */
        #somethingorother { width: 800px ; }

        @media screen and (max-width: 320px) {
            .leftSideBar, .rightSideBar { display:none; }
          /* comes into effect for screens less than or equal to 320 pixels */
          #somethingorother { width: 120px ; }
        }
        @media screen and (min-width: 321px) and (max-width: 480px) {
            .leftSideBar, .rightSideBar { display:none; }
          /* comes into effect for screens between 321 and 480 pixels (inclusive) */
          #somethingorother { width: 320px ; }
        }
        @media screen and (min-width: 481px) {
            .leftSideBar, .rightSideBar { display:none; }
          /* comes into effect for screens larger than or equal to 481 pixels */
          #somethingorother { width: 480px ; }
        }
    </style>

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>--%>
    <script src="jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divMessage').fadeOut(10000); // 10 seconds x 1000 milisec = 10000 milisec
        });

        $(document).load(function () {
            $("#<%=btnSubmit.ClientID %>").click(function () {
                ShowSendingProgress();
            });
        });

        function ShowSendingProgress() {
		    var modal = $('<div  />');
            modal.addClass("modal");
            modal.attr("id", "modalSending");
            $('body').append(modal);
            var loading = $("#modalSending.loading");
            loading.show();
            var top = '150px';
            var left = '90px';
            loading.css({ top: top, left: left, color: '#ffffff' });

            document.getElementById("divMessage").style.visibility = "hidden";
            document.getElementById("txtJson").style.visibility = "hidden";
            document.getElementById("divAd").innerHTML = "";
        }

        function StopProgress() {
            $("div.modal").hide();

            var loading = $(".loading");
            loading.hide();
        }
    </script>
</head>
<body>
    <form id="form1" enctype="multipart/form-data" runat="server">
        <div class="loading" align="center" id="modalSending">
            <img src="images/spinner-s.gif" width="100" />
        </div> 
        <div style="margin-top: 5px; width: 100%;">
            <table width="100%" class="border">
                <tr>
                    <td style="width: 50%;">
                        <div style="text-align: left;" class="title">Ready Demo 2017</div>
                    </td>
                    <td>
                        <div style="text-align: right;">
                            <img width="100" itemprop="logo" itemscope="itemscope" class="c-image" alt="Microsoft" src="https://assets.onestore.ms/cdnfiles/external/uhf/long/9a49a7e9d8e881327e81b9eb43dabc01de70a9bb/images/microsoft-gray.png" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding: 50px 50px; width: 100%; padding-top: 15px; padding-left: 15px;">
            <table width="100%">
                <tr>
                    <td style="width: 50%;">
                        <div>
                            File: 
                        </div>
                        <div>
                            <asp:FileUpload ID="fileUpload1" runat="server" Width="250px" />
                        </div>
                        <div style="padding-top: 10px;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Upload" class="button" OnClick="btnSubmit_Click" OnClientClick="javascript:return ShowSendingProgress();" />
                            <br />
                            <br />
                            <div id="txtJson" runat="server" visible="false">
                            Output: 
                            <br /><textarea id="txtareaJson" runat="server" cols="30" rows="16"></textarea>
                            </div>
                            <div class="alert" id="divMessage" runat="server"></div>
                            <br />
                            <div id="divAd" runat="server"></div>
                        </div>
                        <div class="bottomleft">
                            <img src="images/e4b88be8bc89.png" width="80" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
