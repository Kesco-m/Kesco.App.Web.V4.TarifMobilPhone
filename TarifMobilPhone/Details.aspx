<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Kesco.App.Web.TarifMobilPhone.Details" %>
<%@ Register TagPrefix="csg" Namespace="Kesco.Lib.Web.Controls.V4.Grid" Assembly="Controls.V4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=TMD_Title%></title>
     <script src="/styles/Kesco.V4/JS/jquery.floatThead.min.js" type="text/javascript"></script>
     <script src="/styles/Kesco.V4/JS/Kesco.Grid.js" type="text/javascript"></script>
</head>
<body>
    
    <div class="v4formContainer">
        <div id="divTitle" style="MARGIN-TOP:10px;MARGIN-BOTTOM:10px;"></div> 
        <csg:Grid runat="server" ID="gridData" MarginBottom="170"></csg:Grid>
        
         <fieldset id="divLegenda" style="margin-top:20px;margin-left:10px; width: 600px;">
            <legend><%=LegendTitle%>:</legend><br>
            <nobr><span style="width:20px; background:white; border: 1px solid black">&nbsp;&nbsp;&nbsp;</span><span> - <%=TitleWhite%></span></nobr><br>
            <nobr><span style="width:20px; background:lightgray; border: 1px solid black">&nbsp;&nbsp;&nbsp;</span><span> - <%=TitleGray%></span></nobr><br>
            <nobr><span style="width:20px; background:darkorange; border: 1px solid black">&nbsp;&nbsp;&nbsp;</span><span> - <%=TitleOrange%></span></nobr>
        </fieldset>
     </div>

     <div id="TimeSpan" style="margin-left: 10px;"></div>
</body>
<script language="javascript">

    function PrintData() {
        window.open("details.aspx?idpage=" + idp + "&view=print", "_blank", "toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes");
    }
</script>

</html>
