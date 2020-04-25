<%@ Page Language="VB" MasterPageFile="~/DefaultMasterPAge.master" AutoEventWireup="false" CodeFile="Cobertura.aspx.vb" Inherits="Cobertura" title="Cobertura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">
    <script type="text/javascript" src="Scripts/jquery-1.7.2.js"></script>
    <script type="text/javascript" src="Scripts/Raphael.js"></script>
    <script type="text/javascript" src="Scripts/Cobertura.js"></script>    
    
    <style type="text/css">
        body {
	        background: #FFFFFF;
        }
        .row {
            width: 790px;
            /*border:1px solid gray;*/
            display:table;
            word-spacing: normal;
            letter-spacing: normal;
            font-size: small;
            padding: 10px 2px 10px 2px;
            font-family: Arial, Helvetica, sans-serif;
            margin-left: -40%;
	        /*-moz-box-shadow: 1px 1px 3px #999;
	        -webkit-box-shadow:	1px 1px 3px #999;*/
        }
        .r1-left {
            width:500px;
            height: 710px;
            /*border:1px solid gray;*/
            padding:3px;
            float: left;
            text-align: left;
        }

        .r1-right {
            width: 250px;
            /*border: 1px solid green;*/
            float:left;
            padding:3px;
            text-align:left;
            margin-top: 11%;
        }
        #mapa_mexico {
	        width: 500px;
	        height: 400px;
	        margin: 50px auto;
            margin-top: 150px;
        }
         #Ciudades {
            float: left;
            width: 250px;
            height: 305px;
            /*margin-top: -130%;
            margin-left: 500px;*/
            border:0px solid gray;
            word-spacing: normal;
            letter-spacing: normal;
            font-size: small;
            padding: 5px 2px 5px 2px;
	        -moz-box-shadow: 1px 3px 6px #999;
	        -webkit-box-shadow:	1px 3px 6px #999;
            border-radius: 8px;
            background: rgb(239, 239, 239);
             overflow: auto;
        }
    </style>
        <div class="row" align="center">
            <div class="r1-left">
                <div id="mapa_mexico">
                    <div id="chaptersMap">
                    </div>
                </div>
            </div>
            <div class="r1-right" style="text-align: right">
                <asp:ListBox ID="Ciudades" runat="server" Rows="80" ClientIDMode="Static" SelectionMode="Multiple" ></asp:ListBox>
            </div>
        </div>
</asp:Content>