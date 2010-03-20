Imports System.IO

Public Class Form

    Dim DomainsList As List(Of String)
    Dim ProjectsLists As List(Of List(Of String))


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Populate.Click

        SetLoginTime()
        DefectsList = New TDAPIOLELib.List
        ReqsList = New TDAPIOLELib.List
        TestsList = New TDAPIOLELib.List
        TestSetsList = New TDAPIOLELib.List

        If LoggerCheckBox.Checked Then
            InitializeLogFile()
        End If

        LogState = True 'start redirect all text in result to the log file
        ProgressBar.Value = 0
        PingServer()
        Try 'connecting to the selected project
            Dim ConnectDuration As TimeSpan
            Result.Text = "Connecting to " & ProjectsComboBox.SelectedText & " Project"
            ConnectDuration = Now.TimeOfDay
            Connect()
            ProgressBar.Increment(10)
            ConnectDuration = Now.TimeOfDay - ConnectDuration
            Result.Text = "Login Completed Successfully." & vbCrLf & "Duration: " & ConnectDuration.ToString.Substring(0, 8)
            Refresh()

        Catch ex As Exception
            Result.Text = "Can't connect to Server"
            AddEventToLog(1, "Can't connect to Server")
            Return
        End Try

        'Check Data Integrity
        Check_Data_integrity()

        Try 'start populating
            Dim PopulateDuration As TimeSpan
            PopulateDuration = Now.TimeOfDay
            AddEventToLog(1, "Start populating")


            'Handle Defects
            If DefectsCheckBox.Checked = True Then
                'start stopper
                Dim DefectDuration = Now.TimeOfDay
                Handle_Defects()
                'stop stopper
                DefectDuration = Now.TimeOfDay - DefectDuration

                Result.Text = "Creating Defects Completed Successfully" & vbCrLf & "Duration: " & DefectDuration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle Requirements
            If ReqCheckBox.Checked = True Then
                'start stopper
                Dim RequirementDuration = Now.TimeOfDay
                Handle_Requirements()
                'stop stopper
                RequirementDuration = Now.TimeOfDay - RequirementDuration
                Result.Text = "Creating Requirements Completed Successfully" & vbCrLf & "Duration: " & RequirementDuration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle Tests
            If TestCheckBox.Checked = True Then
                'start stopper
                Dim TestsDuration = Now.TimeOfDay
                Handle_Tests()
                'stop stopper
                TestsDuration = Now.TimeOfDay - TestsDuration
                Result.Text = "Creating Tests Completed Successfully" & vbCrLf & "Duration: " & TestsDuration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle Test Lab
            If TestLabCheckBox.Checked Then
                'start stopper
                Dim TestSetsDuration = Now.TimeOfDay
                Handle_Test_Sets()
                'stop stopper
                TestSetsDuration = Now.TimeOfDay - TestSetsDuration
                Result.Text = "Creating Test Sets Completed Successfully" & vbCrLf & "Duration: " & TestSetsDuration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle defect-defect link
            If DefectsCheckBox.Checked = True Then
                'Start stopper
                Dim Def_Def_Link_Duration = Now.TimeOfDay
                Handle_Defects_Relations()
                'stop stopper
                Def_Def_Link_Duration = Now.TimeOfDay - Def_Def_Link_Duration
                Result.Text = "Creating Defects-Defects Linkage Completed Successfully" & vbCrLf & "Duration: " & Def_Def_Link_Duration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle reqs-reqs link
            If ReqCheckBox.Checked = True Then
                'Start stopper
                Dim Req_Req_Link_Duration = Now.TimeOfDay
                Handle_req_traceability()
                'stop stopper
                Req_Req_Link_Duration = Now.TimeOfDay - Req_Req_Link_Duration
                Result.Text = "Creating Requirements traceability Completed Successfully" & vbCrLf & "Duration: " & Req_Req_Link_Duration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle reqs-defect link
            If ReqCheckBox.Checked = True And DefectsCheckBox.Checked = True Then
                'Start stopper
                Dim Req_Def_Link_Duration = Now.TimeOfDay
                Handle_Requirements_defects_linkage()
                'stop stopper
                Req_Def_Link_Duration = Now.TimeOfDay - Req_Def_Link_Duration
                Result.Text = "Creating Requirements-defects linkage Completed Successfully" & vbCrLf & "Duration: " & Req_Def_Link_Duration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle tests-defects link
            If TestCheckBox.Checked = True And DefectsCheckBox.Checked = True Then
                'start stopper
                Dim Tests_def_Link_Duration = Now.TimeOfDay
                Handle_Tests_defects_linkage()
                Tests_def_Link_Duration = Now.TimeOfDay - Tests_def_Link_Duration
                Result.Text = "Creating Tests-defects linkage Completed Successfully" & vbCrLf & "Duration: " & Tests_def_Link_Duration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle Req-test links
            If ReqCheckBox.Checked And TestCheckBox.Checked Then
                'start stopper
                Dim Req_Test_Cov_Duration = Now.TimeOfDay
                Handle_Req_test_coverage()
                Req_Test_Cov_Duration = Now.TimeOfDay - Req_Test_Cov_Duration
                Result.Text = "Creating Tests-Requirements linkage Completed Successfully" & vbCrLf & "Duration: " & Req_Test_Cov_Duration.ToString.Substring(0, 8)
                ProgressBar.Increment(10)
                Refresh()
            End If

            'stop stopper
            PopulateDuration = Now.TimeOfDay - PopulateDuration
            Dim ppp = PopulateDuration.ToString
            Result.Text = "Populating Completed Successfully." & vbCrLf & "Duration: " & PopulateDuration.ToString.Substring(0, 8)

        Catch exp As Exception
            Result.Text = exp.Message

        Finally
            ProgressBar.Value = 100
            Refresh()
            If LoggerCheckBox.Checked = True Then
                LogFile.Close()
            End If
            LogState = False 'the file is closed so don't record the disconnection
        End Try

    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tdc = New TDAPIOLELib.TDConnection
        LogFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PDPLogger" & ".html"
        LogTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PDPLogger" & ".html"
        LogState = False
    End Sub

    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click

        Try
            ProgressBar.Value = 0
            Disconnect()
            Dim Login_time As TimeSpan
            Result.Text = "Logging in to QC Server. Please Wait..."
            Refresh()
            Login_time = Now.TimeOfDay
            Login()
            Login_time = Now.TimeOfDay - Login_time
            Result.Text = "Logged in." & vbCrLf & "Duration: " & Login_time.ToString.Substring(0, 8)
            Populate_Login_lists()
            Populate_Domain_ComboBox()
            Refresh()

        Catch ex As Exception
            Disconnect()
            Result.Text = ex.Message
            Result.ForeColor = Color.Maroon
        End Try
    End Sub

    Private Sub Populate_Login_lists()

        ProjectsLists = New List(Of List(Of String))
        DomainsList = New List(Of String)

        ProjectsLists.Clear()
        DomainsList.Clear()

        Dim projectdesc As TDAPIOLELib.ProjectDescriptor
        For i = 1 To tdc.GetAllVisibleProjectDescriptors().Count
            projectdesc = tdc.GetAllVisibleProjectDescriptors().Item(i)
            If (DomainsList.IndexOf(projectdesc.DomainName) = -1) Then 'if domain not in the domain lists, add it
                DomainsList.Add(projectdesc.DomainName)
            End If
            ' Add the project to the corresponding list in projects lists
            Dim ind As Integer = DomainsList.IndexOf(projectdesc.DomainName)
            ProjectsLists.Add(New List(Of String))
            ProjectsLists.Item(ind).Add(projectdesc.Name)

        Next

    End Sub

    Private Sub Populate_Domain_ComboBox()

        DomainComboBox.Items.Clear()
        For i = 0 To DomainsList.Count - 1
            DomainComboBox.Items.Add(DomainsList(i))
        Next
        DomainComboBox.SelectedIndex = 0
    End Sub

    Private Sub DomainComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DomainComboBox.SelectedIndexChanged
        ProjectsComboBox.Items.Clear()
        ProjectsComboBox.SelectedText = ""
        ProjectsComboBox.SelectedValue = ""
        Dim SelectedDomainIndexInList = DomainComboBox.SelectedIndex 'comboindex = listindex+1
        For i = 0 To ProjectsLists(SelectedDomainIndexInList).Count - 1
            ProjectsComboBox.Items.Add(ProjectsLists(SelectedDomainIndexInList)(i))
        Next
        ProjectsComboBox.SelectedIndex = 0
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Disconnect()
    End Sub

    Private Sub Update_Low_TextBox()
        Dim newvalue = 100 - Val(CriticalTextBox.Text) - Val(VeryHighTextBox.Text) - Val(HighTextBox.Text) - Val(MeduimTextBox.Text)
        If newvalue < 0 Then
            MessageBox.Show("ILLIGAL DISTRIBUTION")
            Return
        End If
        LowTextBox.Text = newvalue.ToString
    End Sub

    Private Sub HighTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HighTextBox.TextChanged
        Update_Low_TextBox()
    End Sub

    Private Sub MeduimTextBox_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MeduimTextBox.TextChanged
        Update_Low_TextBox()
    End Sub

    Private Sub CriticalTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CriticalTextBox.TextChanged
        Update_Low_TextBox()
    End Sub

    Private Sub VeryHighTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VeryHighTextBox.TextChanged
        Update_Low_TextBox()
    End Sub
    Private Sub Update_New_TextBox()
        Dim newvalue = 100 - Val(OpenTextBox.Text) - Val(FixTextBox.Text) - Val(CloseTextBox.Text) - Val(ReopenTextBox.Text) - Val(RejectTextBox.Text)
        If newvalue < 0 Then
            MessageBox.Show("ILLIGAL DISTRIBUTION")
            Return
        End If
        NewTextBox.Text = newvalue.ToString
    End Sub


    Private Sub OpenTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenTextBox.TextChanged
        Update_New_TextBox()
    End Sub

    Private Sub FixTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FixTextBox.TextChanged
        Update_New_TextBox()
    End Sub

    Private Sub ClosedTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseTextBox.TextChanged
        Update_New_TextBox()
    End Sub

    Private Sub ReopenTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReopenTextBox.TextChanged
        Update_New_TextBox()
    End Sub

    Private Sub RejectTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectTextBox.TextChanged
        Update_New_TextBox()
    End Sub

    Public Function GetRandomInt(ByVal Low, ByVal High) As Integer
        Randomize()
        Dim Rand = Int((High - Low + 1) * Rnd()) + Low
        Return Rand
    End Function

    Private Sub CommonCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttachmentCheckBox.CheckedChanged
        If AttachmentCheckBox.Checked Then
            DefectAttachment.Enabled = True
            ReqAttachment.Enabled = True
            TestPlanAttachement.Enabled = True
            TestLabAttachment.Enabled = True
            AttachementTextBox.Enabled = True
            AttachmentBrowse.Enabled = True
        Else
            DefectAttachment.Enabled = False
            ReqAttachment.Enabled = False
            TestPlanAttachement.Enabled = False
            TestLabAttachment.Enabled = False
            AttachementTextBox.Enabled = False
            AttachmentBrowse.Enabled = False
        End If

    End Sub

    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttachmentBrowse.Click
        OpenFD.Title = "Load Attachment File"
        OpenFD.InitialDirectory = Environment.SpecialFolder.Desktop.ToString()
        Dim Chosen_File As String = ""
        OpenFD.FileName = ""
        OpenFD.Filter = "ALL Files|*.*"
        If (OpenFD.ShowDialog <> DialogResult.Cancel) Then
            Chosen_File = OpenFD.FileName
        End If
        AttachementTextBox.Text = Chosen_File

    End Sub


    Private Sub CommonTabPage_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommonTabPage.Leave
        If AttachmentCheckBox.Checked And Not File.Exists(AttachementTextBox.Text) Then
            MessageBox.Show("File does not exist, please make sure the file exist in the local file system")
            AttachmentCheckBox.Checked = False
            AttachementTextBox.Text = ""
        End If
    End Sub
    Private Sub SaveLogButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLogBrowse.Click
        If LoggerCheckBox.Checked Then
            SaveLogFD.InitialDirectory = Environment.SpecialFolder.Desktop.ToString()
            SaveLogFD.Title = "Save Log As"
            SaveLogFD.FileName = "PDPLogger"
            SaveLogFD.Filter = "HTML Files|*.html"
            If (SaveLogFD.ShowDialog() <> DialogResult.Cancel) Then
                LogFileName = SaveLogFD.FileName
                LogTextBox.Text = SaveLogFD.FileName
            End If
        End If

    End Sub

    Private Sub LoggerCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoggerCheckBox.CheckedChanged
        If LoggerCheckBox.Checked Then
            LogTextBox.Enabled = True
            SaveLogBrowse.Enabled = True
        Else
            LogTextBox.Enabled = False
            SaveLogBrowse.Enabled = False
        End If
    End Sub

    Private Sub Result_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Result.TextChanged

        If LogState = True And LoggerCheckBox.Checked Then
            If Result.Text.Contains("Duration") Then
                AddEventToLog(2, Result.Text)
            Else
                AddEventToLog(1, Result.Text)
            End If
        End If
    End Sub

    Private Function GetMemoryUsage() As Integer
        Return GC.GetTotalMemory(False)
    End Function

    Private Sub Check_Data_integrity()

    End Sub


End Class


