<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details_Old.aspx.cs" Inherits="Kesco.App.Web.TarifMobilPhone.Details_Old" %>
<%@ Register TagPrefix="cs" Namespace="Kesco.Lib.Web.Controls.V4" Assembly="Controls.V4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=TMD_Title%></title>
<style type="text/css" >
     
    .tdR{text-align:right} 
    .tdC{text-align:center} 
    .btn { cursor: pointer; }
    
    #divMain table{font-size:8pt; border:0}
    #divMain th, #divMain td {padding: 3px;border: 1px solid #efefef ;}
</style>

</head>

<body style="margin-left: 20px">

    <div id="divTitle" style="MARGIN-TOP:10px;MARGIN-BOTTOM:10px;"></div> 
    <div id="divWait" style="display: none;"><img src="/styles/ProgressBar.gif"/>Подождите...</div>
    <div id="divMain" style="MARGIN-RIGHT:25px;display: block;"></div>
    <fieldset id="divLegenda" style="margin-top:20px; width: 600px;">
        <legend><%=LegendTitle%>:</legend><br>
        <nobr><span style="width:20px; background:white; border: 1px solid black">&nbsp;&nbsp;&nbsp;</span><span> - <%=TitleWhite%></span></nobr><br>
        <nobr><span style="width:20px; background:lightgray; border: 1px solid black">&nbsp;&nbsp;&nbsp;</span><span> - <%=TitleGray%></span></nobr><br>
        <nobr><span style="width:20px; background:darkorange; border: 1px solid black">&nbsp;&nbsp;&nbsp;</span><span> - <%=TitleOrange%></span></nobr>
    </fieldset>

<script language="javascript">

    function PrintData() {
        window.open("details.aspx?idpage=" + idp + "&view=print", "_blank", "toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes");
    }

    function displayWait(d) {
        var dv = document.getElementById("divWait");
        if (dv) dv.style.display = d;
    }

    function SortData(column) {

        Wait.render(true);
        cmdasync('cmd', 'Sort', 'Column', column);
    }

    function set_tBody(new_html) {
        var $tbl = $('#tblMain');
        var $newBody = $("<tbody id='tblBody'></tbody>").append($(new_html));
        $tbl.find('tbody').replaceWith($newBody);
    }

    function set_tFoot(new_html) {
        var $tbl = $('#tblMain');
        var $newBody = $("<tfoot id='tblFoot'></tfoot>").append($(new_html));
        $tbl.find('tfoot').replaceWith($newBody);
    }

    function ServerOnChange(cmdName, column, value) {
        if (column == null) column = "";
        if (value == null) value = "";
        Wait.render(true);
        cmdasync('cmd', cmdName, 'Column', column, 'Value', value);
    }

    function runCmdAsync(name) {
        Wait.render(true);
        cmdasync('cmd', name);
    }

</script>

</body>
</html>
