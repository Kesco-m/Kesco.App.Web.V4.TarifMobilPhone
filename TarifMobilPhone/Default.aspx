<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Kesco.App.Web.TarifMobilPhone.DefaultPage" %>
<%@ Register TagPrefix="cs" Namespace="Kesco.Lib.Web.Controls.V4" Assembly="Controls.V4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">


    <title><%= TM_Title %></title>
    <script src="/Styles/sizzle.min.js" type="text/javascript"></script>
    <style>
        table {
            border: 0;
            font-size: 8pt;
        }

        li { PADDING: 5px }

        tr.level td { border-bottom: 1pt solid #efefef; }

        .level3 { border-bottom: gray 1px solid; }

        .levelBtn {
            height: 16px;
            width: 16px;
        }

        .btn { cursor: pointer; }

        option[value=""] { color: gray; }

        option[value="-1"] { color: gray; }

        option[value="0"] { color: gray; }

        .tooltip {
            background-color: #fafad2;
            border: 1px solid #fff;
            left: -9999px;
            padding: 5px;
            position: absolute;
            z-index: 999;
        }

        [name="phone"] { cursor: help; }    
        
    </style>
    <script type="text/javascript">
        var currentLevel = 1;
    </script>
</head>
<body style="margin-left: 25px; margin-top: 10px;">
<table style="MARGIN-TOP: 10px" id="tblMain" cellpadding="2" cellspacing="0">
    <tr>
        <td align="left" valign="top" colspan="2">
            <h3><%= TM_Title %></h3>
        </td>
        <td align="right" valign="top">
            <a href="javascript:void(0);" onclick="v4_openHelp('<%= IDPage %>');" class="btn" title="<%= Title_Help %>">
                <img src="/styles/Help.gif" border="0">
            </a>
        </td>
    </tr>
    <tr id="tr1" style="margin-bottom: 10">
        <td colspan="2">
            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td noWrap><%= TM_FOpenMonth %>:</td>
                    <td>
                        <cs:CheckBox runat="server" ID="chOpenMonth" HtmlID="chOpenMonth" TabIndex="0"></cs:CheckBox>
                    </td>

                    <td style="text-align: right; width: 100%;" align="right">
                        <a href="javascript:void(0);" onclick="runCmdAsync('FilterClear');" class="btn"><img src="/styles/delete.gif" border="0">&nbsp;<%= TM_ClearFilter %></a>
                        <a href="javascript:void(0);" onclick="runCmdAsync('FilterApply');" class="btn"><img src="/styles/filterapply.gif" border="0">&nbsp;<%= TM_FilterApply %></a>
                    </td>
                </tr>
            </table>
        </td>
        <td rowspan="6" style="PADDING-LEFT: 15px">
            <a href="javascript:void(0);" onclick="openRpt(0, 'Затраты по телефонам');" class="btn">
                <img border="0" src="/styles/chart.gif"> <%= TM_Rpt1 %>
            </a><br>
            <a href="javascript:void(0);" onclick="openRpt(1, 'Длительность по телефонам');" class="btn">
                <img border="0" src="/styles/chart.gif"> <%= TM_Rpt2 %>
            </a> <br>
            <a href="javascript:void(0);" onclick="openRpt(2, 'Затраты по сотрудникам');" class="btn">
                <img border="0" src="/styles/chart.gif"> <%= TM_Rpt3 %>
            </a> <br>
            <a href="javascript:void(0);" onclick="openRpt(3, 'Длительность по сотрудникам');" class="btn">
                <img border="0" src="/styles/chart.gif"> <%= TM_Rpt4 %>
            </a> <br><br>
            <a href="javascript:void(0);" onclick="openDetailsBeeline();" class="btn" style="display: none">
                <img border="0" src="/styles/detail.gif"> <%= TM_Rpt5 %>
            </a>
        </td>
    </tr>
    <tr id="tr2" style="DISPLAY: none;">
        <td><%= TM_FYear %>:</td>
        <td style="width: 300px">
            <cs:ComboBox runat="server" ID="cbYear" HtmlID="cbYear" Width="85px" TabIndex="1"></cs:ComboBox>
        </td>
    </tr>
    <tr id="tr3" style="DISPLAY: none;">
        <td><%= TM_FMonth %>:</td>
        <td >
            <cs:ComboBox runat="server" ID="cbMonth" HtmlID="cbMonth" Width="200px" TabIndex="2"></cs:ComboBox>
        </td>
    </tr>
    <tr id="tr4">
        <td><%= TM_FDogovor %>:</td>
        <td >
            <cs:ComboBox runat="server" ID="cbDogovor" HtmlID="cbDogovor" width="300px" TabIndex="3"></cs:ComboBox>
        </td>
    </tr>
    <tr id="tr5">
        <td><%= TM_FEmployee %>:</td>
        <td>
            <cs:ComboBox runat="server" ID="cbEmployee" HtmlID="cbEmployee" width="300px" TabIndex="4"></cs:ComboBox>
        </td>
    </tr>
    <tr id="tr6">
        <td><%= TM_FPhone %>:</td>
        <td >
            <cs:ComboBox runat="server" ID="cbPhone" HtmlID="cbPhone" width="300px" TabIndex="5" EmptyValueText="- все телефоны -"></cs:ComboBox>
        </td>
    </tr>
</table>

<div id="divMain">
    <div id="divLevel" style="display: none;">
        <button class="levelBtn btn" id="btnLevelOne" onclick="displayLevel(1);" style="BACKGROUND: url(/styles/LevelOne.gif) buttonface no-repeat center center"></button>
        <button class="levelBtn btn" id="btnLevelTwo" onclick="displayLevel(2);" style="BACKGROUND: url(/styles/LevelTwo.gif) buttonface no-repeat center center"></button>
        <button class="levelBtn btn" id="btnLevelThree" onclick="displayLevel(3);" style="BACKGROUND: url(/styles/LevelThree.gif) buttonface no-repeat center center"></button>
    </div>
    <div id="divResult">
    </div>

    <fieldset id="divLegenda" style="display: none; margin-top: 20px; width: 600px;">
        <legend><%= LegendTitle %>:</legend><br>
        <nobr>
            <span style="background: white; border: 1px solid black; width: 20px;">&nbsp;&nbsp;&nbsp;</span><span> - <%= TitleWhite %></span>
        </nobr><br>
        <nobr>
            <span style="background: lightgray; border: 1px solid black; width: 20px;">&nbsp;&nbsp;&nbsp;</span><span> - <%= TitleGray %></span>
        </nobr><br>
        <nobr>
            <span style="background: darkorange; border: 1px solid black; width: 20px;">&nbsp;&nbsp;&nbsp;</span><span> - <%= TitleOrange %></span>
        </nobr>
    </fieldset>
    <div id="divEmpty" style="margin-top: 20px;"><img src="/styles/warning.gif"/>&nbsp;<%= TM_Empty %></div>
</div>


</body>
<script type="text/javascript">


    function openRpt(inx, text) {
        cmd('cmd', 'OpentRpt', 'inx', inx);
    }

    function openDetailsBeeline() {
        cmd('cmd', 'OpentBln');
    }

    function runCmdAsync(name) {
        cmdasync('cmd', name);
    }


    function openDetailItem(pOpenMonth,
        pYear,
        pMonth,
        pDogovor,
        pDogovorT,
        pUser,
        pPhone,
        bln,
        pNDSAllSumm,
        pNDSStavka,
        pNDSIn,
        pScale,
        pColor,
        pTitle,
        pUserT) {

        var url = "details.aspx";
        url += "?OpenMonth=" + pOpenMonth;
        url += "&Year=" + pYear;
        url += "&Month=" + pMonth;
        url += "&Dogovor=" + pDogovor;
        url += "&DogovorT=" + encodeURIComponent(pDogovorT);
        url += "&User=" + pUser;
        url += "&UserT=" + encodeURIComponent(pUserT);
        url += "&Phone=" + pPhone;
        url += "&bln=" + bln;

        url += "&NDSAllSumm=" + pNDSAllSumm;
        url += "&NDSIn=" + pNDSIn;
        url += "&NDSStavka=" + encodeURIComponent(pNDSStavka);
        url += "&Scale=" + pScale;

        url += "&Color=" + encodeURIComponent(pColor);
        url += "&Title=" + encodeURIComponent(pTitle);

        v4_windowOpen(url,
            '_blank',
            'width=1180,height=700,menubar=no,location=no,resizable=yes,scrollbars=yes,status=yes');
    }

    function displayLevel(inx) {
        currentLevel = inx;

        var img;
        var level;
        var trs = Sizzle(".level");

        for (var i = 0; i < trs.length; i++) {

            level = trs[i].getAttribute('level');
            img = Sizzle(".levelImg", trs[i]);

            if (parseInt(level) > currentLevel)
                trs[i].style.display = 'none';
            else if (parseInt(level) <= currentLevel && parseInt(level) > 1)
                trs[i].style.display = 'table-row';

            if (parseInt(level) < currentLevel) {
                if (img.length > 0) img[0].src = '/styles/minus.gif';
            } else {
                if (img.length > 0) img[0].src = '/styles/plus.gif';
            }
        }

        cmdasync('cmd', 'SetLevel', 'CurrentLevel', currentLevel);
    }

    function displayLevelItem(trId) {

        var img;
        var childs;
        var disp = '';

        var trs = Sizzle("[name=" + trId + "]");

        for (var i = 0; i < trs.length; i++) {

            if (i == 0) disp = trs[i].style.display == 'none' ? 'table-row' : 'none';
            trs[i].style.display = disp;

            if (disp == 'none') {

                img = Sizzle(".levelImg", trs[i]);

                if (img.length > 0)
                    img[0].src = '/styles/plus.gif';

                childs = Sizzle("[name=" + trs[i].id + "]");
                for (var j = 0; j < childs.length; j++)
                    childs[j].style.display = disp;
            }
        }

        var tr = document.getElementById(trId);
        if (tr == null) return;

        img = Sizzle(".levelImg", tr);

        if (img.length > 0)
            img[0].src = disp == 'none' ? '/styles/plus.gif' : '/styles/minus.gif';

    }

    /*Tooltip*/

    var tooltipRegistry = {};

    function createTooltip(name) {
        $(".tooltip").remove();
        $("span[name=" + name + "]").each(function(i) {
            var data = $(this).attr('data');
            if (data == null) return;
            var id = "tp" + data;

            $("body").append("<div class='tooltip' id='" + id + "' style='display:none;'>" + "</div>");
            var my_tooltip = $("#" + id);

            $(this)
                .mouseover(function() {
                    tooltipRegistry[data] = data;
                    window.setTimeout(function() {
                            if (tooltipRegistry[data] == null) return;
                            my_tooltip.css({ display: "none" }).fadeIn(0);
                            var obj = document.getElementById(id);
                            if (obj == null) return;
                            var objAttr = $(obj).attr("data");

                            if (objAttr != null) {
                                if (obj.innerHTML.length == 0)
                                    obj.style.display = 'none';
                                return;
                            }
                            loadTooltipValue(id, data);
                        },
                        500);

                })
                .mousemove(function(kmouse) {
                    if (tooltipRegistry[data] == null) return;
                    my_tooltip.css({
                        left: kmouse.pageX + 20,
                        top: kmouse.pageY + 5
                    });

                })
                .mouseout(function() {
                    delete tooltipRegistry[data];
                    my_tooltip.fadeOut(0);
                });

        });
    }

    function loadTooltipValue(id, data) {

        var obj = document.getElementById(id);
        if (obj == null) return;
        obj.style.display = 'block';
        obj.innerHTML = "<%= TM_Wait %>";

        window.setTimeout(function() { cmdasync('cmd', 'RenderSim', 'id', id, 'data', data); }, 500);
    }

    function setTooltipValue(id, data, context) {
        delete tooltipRegistry[data];
        var obj = document.getElementById(id);
        if (obj == null) return;
        $(obj).attr("data", data);
        obj.innerHTML = context;
        if (data.length == 0)
            obj.style.display = 'none';
    }

</script>
</html>