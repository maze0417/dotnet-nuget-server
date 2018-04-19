<%@ Page Language="C#" %>

<%@ Import Namespace="System.IO" %>
<html>
<head>
    <title>List of NuGet Packages</title>
    <style>
        * {
            font-family: sans-serif;
        }
    </style>
</head>
<body>

    <table border="1" cellpadding="5" cellspacing="0" style="border-collapse: collapse">
        <tr>
            <th>Package Name
            </th>
            <th>Version
            </th>
            <th>Last Modified
            </th>
            <th>Created
            </th>
        </tr>

        <%
            var result = from packDir in Directory.GetDirectories(Server.MapPath("~/Packages"))
                         from version in Directory.GetDirectories(packDir)
                         select new
                         {
                             Name = new DirectoryInfo(packDir).Name,
                             Version = new DirectoryInfo(version).Name,
                             UpdateOn = Directory.GetLastWriteTime(version).ToString("MMMM, dd, yyyy hh:mm:ss"),
                             CreatedOn = Directory.GetCreationTime(version).ToString("MMMM, dd, yyyy hh:mm:ss")
                         };

            foreach (var packPath in
                result.OrderByDescending(f => f.UpdateOn)
                .Take(150))
            {
        %>
        <tr>
            <td>
                <a href="https://feed.ugsdev.com/api/v2/package/<%=packPath.Name%>/<%=packPath.Version%>">
                    <%=packPath.Name%>
                </a>
            </td>
            <td>
                <b>
                    <%=packPath.Version%>
                </b>
            </td>
            <td>
                <%=packPath.UpdateOn%>
            </td>
            <td>
                <%=packPath.CreatedOn%>
            </td>
        </tr>

        <%
            }
        %>
    </table>
</body>
</html>