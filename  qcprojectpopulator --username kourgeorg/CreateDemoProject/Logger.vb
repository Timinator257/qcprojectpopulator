Module Logger
    Public LogFileName As String
    Public LogFile As System.IO.StreamWriter
    Public log As String
    Public LogState As Boolean 'Determines wheher to redirect Result.Text to log file

    Public Sub InitializeLogFile()
        'Create the log file in the filesystem
        Dim ActualLogFileName As String
        LogFileName = Form.LogTextBox.Text
        ActualLogFileName = LogFileName
        Dim k = 1
        While System.IO.File.Exists(ActualLogFileName)
            ActualLogFileName = LogFileName.Insert(LogFileName.IndexOf("."), "(" & k.ToString & ")")
            k += 1
        End While

        LogFile = New System.IO.StreamWriter(ActualLogFileName, True)
        LogFile.WriteLine("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN"" ""http://www.w3.org/TR/html4/loose.dtd"">")
        LogFile.WriteLine("<html>")
        LogFile.WriteLine("<head>")
        LogFile.WriteLine("<title>QC Project Data Populator Logger</title>")
        LogFile.WriteLine("<p style=""color: #3A3A3A; font-size: xx-large; font-weight: bold; font-style: italic"">QC Project Data Populator</p>")
        LogFile.WriteLine("<p style=""color: #224466; font-size: medium; font-weight: bold"">HP Software R&D Internal</p>")
        LogFile.WriteLine("<style type=""text/css"">")
        LogFile.WriteLine("</style>")
        LogFile.WriteLine("</head>")
        LogFile.WriteLine("<body bgcolor=""#FFFFFF"" topmargin=""6"" leftmargin=""6"">")
        LogFile.WriteLine("<hr size=""1"" noshade>")
        LogFile.WriteLine("Log session start time: " & LoginTime & "<br><br>")
        LogFile.WriteLine("<table cellspacing=""0"" cellpadding=""4"" border=""1"" bordercolor=""#224466"" width=""100%"">")
        LogFile.WriteLine("<tr> <th>Time</th><th>Category</th><th>Message</th></tr>")

    End Sub


    Public Sub AddEventToLog(ByVal Category As Integer, ByVal Message As String)
        If Form.LoggerCheckBox.Checked Then
            LogFile.WriteLine("<tr>")
            Dim NowTime = Now.TimeOfDay.ToString()
            If NowTime.Length >= 12 Then
                NowTime = NowTime.Substring(0, 12)
            End If
            Dim NowDay = Now.Date.ToString.Substring(0, 10)
            LogFile.WriteLine("<td>" & NowDay & "  " & NowTime & "</td>")
            If Category = 1 Then
                LogFile.WriteLine("<td title=""Category"">" & "Info" & "</td>")
            Else
                LogFile.WriteLine("<td title=""Category"" style=""color: #0F73A1"">" & "Performance" & "</td>")
            End If
            LogFile.WriteLine("<td title=""Message"">" & Message & "</td>")
            LogFile.WriteLine("</tr>")
        End If
    End Sub

End Module
