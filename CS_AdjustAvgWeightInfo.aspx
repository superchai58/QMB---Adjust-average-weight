<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS_AdjustAvgWeightInfo.aspx.cs" Inherits="CS_AdjustAvgWeightInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background: aliceblue;">
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/style.css" rel="stylesheet" />
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnHead" runat="server" style="background-color: palegoldenrod;">
                <h1 style="text-align:center; padding:10px">ADJUST AVERAGE WEIGHT (F6)</h1>
            </asp:Panel>
            
            <br />
            <div>
                &nbsp;
                <asp:Label ID="lbSN" runat="server" class="FontSetting">Serial number: </asp:Label>
                <asp:TextBox ID="txtSN" runat="server" class="FontSetting"></asp:TextBox>
                &nbsp;
                <asp:Label ID="lbAvg" runat="server" class="FontSetting">Average weight: </asp:Label>
                <asp:TextBox ID="txtAvgWeight" runat="server" class="allow-numeric FontSetting"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="Query" class="btn btn-secondary" style="font-size: 30px;position: absolute;right: 14%;"/>
                <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" class="btn btn-primary" style="font-size: 30px;position: absolute;right: 8%;"/>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" class="btn btn-success" style="font-size: 30px;position: absolute;right: 1%;"/>
            </div>
            <br />
            <div>
                <table class="table table-striped">
                    <tr>
                        <th></th>
                        <th>
                            <h4>PN</h4>
                        </th>
                        <th>
                            <h4>Avg weight</h4>
                        </th>
                        <th>
                            <h4>Unit</h4>
                        </th>
                        <th>
                            <h4>Last update</h4>
                        </th>
                        <th>
                            <h4>Flage</h4>
                        </th>
                        <th>
                            <h4>BoxQty</h4>
                        </th>
                    </tr>
                    <asp:Repeater ID="rptResult" runat="server" OnItemDataBound="rptResult_ItemDataBound">
                        <ItemTemplate>
                            <!-- <asp:RadioButton ID="rdoSelect" runat="server" onclick="fnCheckUnCheck(this.id);" /> -->
                            <tr>
                                <td>
                                    <%--<input id="rdoCheck" type="radio" name="rdoSelect" > --%>
                                    <%--<asp:RadioButton ID="rdoCheck" runat="server" onclick="fnCheckUnCheck(this.id);"/>--%>
                                    <asp:RadioButton ID="rdoCheck" GroupName="GroupName" runat="server"/>
                                </td>
                                <td>
                                    <asp:Label ID="lbPN" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbWeight" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbUnit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbLastUpdate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbFlage" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbBoxQty" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </form>
    <script src="JS/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".allow-numeric").bind("keypress", function (e) {
                var keyCode = e.which ? e.which : e.keyCode
                if (!((keyCode >= 48 && keyCode <= 57) || keyCode == 46)) {
                    $(".error").css("display", "inline");
                    return false;
                } else {
                    $(".error").css("display", "none");
                }
            });
        });

        function setExclusiveRadioButton(strGroupName, current) {
            regex = new RegExp(strGroupName);

            for (i = 0; i < document.forms[0].elements.length; i++) {
                var elem = document.forms[0].elements[i];
                if (elem.type == 'radio') {
                    elem.checked = false;
                }
            }
            current.checked = true;
        }
    </script>

</body>
</html>
