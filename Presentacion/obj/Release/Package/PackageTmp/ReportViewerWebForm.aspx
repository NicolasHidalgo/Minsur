<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReportViewerWebForm.aspx.cs" Inherits="ReportViewerForMvc.ReportViewerWebForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<script src="assets/js/core/libraries/jquery.min.js"></script>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="ReportViewerForMvc" Name="ReportViewerForMvc.Scripts.PostMessage.js" />
                </Scripts>
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  BackColor="#ffffff"></rsweb:ReportViewer>
        </div>
    </form>
<script type="text/javascript">
    $(document).ready(function () {
        $('a[title="Archivo XML con datos de informe"]').parent().hide();
        $('a[title="CSV (delimitado por comas)"]').parent().hide();
        $('a[title="MHTML (archivo web)"]').parent().hide();
        $('a[title="Archivo TIFF"]').parent().hide();

        $('a[title="XML file with report data"]').parent().hide();
        $('a[title="CSV (comma delimited)"]').parent().hide();
        $('a[title="MHTML (web archive)"]').parent().hide();
        $('a[title="Excel"]').parent().hide();
        $('a[title="Word"]').parent().hide();
        $('a[title="TIFF file"]').parent().hide();
    });
</script>

</body>
</html>

