Imports System.Net.NetworkInformation

Module Connection
    Public tdc As TDAPIOLELib.TDConnection
    Public LoginTime As String

    Public Sub Login()
        tdc.InitConnectionEx(Form.ServerURL.Text)
        If (tdc.Connected = True) Then
            tdc.Login(Form.Username.Text, Form.Password.Text)
        End If
    End Sub

    Public Sub Connect()
        tdc.Connect(Form.DomainComboBox.Items(Form.DomainComboBox.SelectedIndex), Form.ProjectsComboBox.Items(Form.ProjectsComboBox.SelectedIndex))
    End Sub

    Public Sub Disconnect()
        Form.Result.Text = "Disconnecting. Please Wait..."
        Form.Refresh()
        If tdc.Connected Then
            If tdc.LoggedIn = True Then
                tdc.Logout()
            End If
            tdc.Disconnect()
        End If
        tdc.ReleaseConnection()
    End Sub

    Public Sub SetLoginTime()
        LoginTime = Now.Date.ToString.Substring(0, 10) & " " & Now.TimeOfDay.ToString.Substring(0, 8)
    End Sub

    Public Sub PingServer()
        Dim ping As Ping = New Ping()
        Dim subs As String = Split(Split(Form.ServerURL.Text, "://")(1), ":")(0)
        Dim RTTAvg As Integer = 0
        For i = 1 To 4
            Dim reply As PingReply = ping.Send(subs)
            If (reply.Status <> IPStatus.Success) Then
                Throw New Exception("Can't Ping Server")
            Else
                AddEventToLog(1, "Ping to server RTT = " + reply.RoundtripTime.ToString)
                RTTAvg += reply.RoundtripTime
            End If
        Next
        RTTAvg = RTTAvg / 4
        AddEventToLog(2, "Average RTT = " + RTTAvg.ToString)
    End Sub

End Module
