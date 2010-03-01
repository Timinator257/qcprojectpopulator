Imports System.Net.NetworkInformation
Imports System.IO

Public Class Form1
    Dim tdc As TDAPIOLELib.TDConnection
    Dim DomainsList As List(Of String)
    Dim ProjectsLists As List(Of List(Of String))
    Dim LoginTime As String

    Dim LogFileName As String
    Dim LogFile As System.IO.StreamWriter
    Dim log As String
    Dim LogState As Boolean 'Determines wheher to redirect Result.Text to log file
    Dim DefectsList As TDAPIOLELib.List
    Dim ReqsList As TDAPIOLELib.List
    Dim TestsList As TDAPIOLELib.List
    Dim TestSetsList As TDAPIOLELib.List


    Private Sub SetLoginTime()
        LoginTime = Now.Date.ToString.Substring(0, 10) & " " & Now.TimeOfDay.ToString.Substring(0, 8)

    End Sub
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

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tdc = New TDAPIOLELib.TDConnection
        LogFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PDPLogger" & ".html"
        LogTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\PDPLogger" & ".html"
        LogState = False
    End Sub
    Private Sub Login()

        tdc.InitConnectionEx(ServerURL.Text)
        If (tdc.Connected = True) Then
            tdc.Login(Username.Text, Password.Text)
        End If

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


    Private Sub Connect()
        tdc.Connect(DomainComboBox.Items(DomainComboBox.SelectedIndex), ProjectsComboBox.Items(ProjectsComboBox.SelectedIndex))
    End Sub

    Private Sub Disconnect()

        Result.Text = "Disconnecting. Please Wait..."
        Refresh()
        If tdc.Connected Then
            If tdc.LoggedIn = True Then
                tdc.Logout()
            End If
            tdc.Disconnect()
        End If
        tdc.ReleaseConnection()
    End Sub

    Private Sub Handle_Defects()

        Dim bfact As TDAPIOLELib.BugFactory
        bfact = tdc.BugFactory
        Dim mybug As TDAPIOLELib.Bug
        Dim high, meduim, low, veryHigh, Critical As Integer
        Critical = (Val(DefectsNum.Text) * Val(CriticalTextBox.Text)) / 100
        veryHigh = (Val(DefectsNum.Text) * Val(VeryHighTextBox.Text)) / 100 + Critical
        high = (Val(DefectsNum.Text) * Val(HighTextBox.Text)) / 100 + veryHigh
        meduim = (Val(DefectsNum.Text) * Val(MeduimTextBox.Text)) / 100 + high
        low = (Val(DefectsNum.Text) * Val(LowTextBox.Text)) / 100 + meduim

        AddEventToLog(1, "start Creating Defects")
        'add defects and determine severity and priority
        For i = 1 To Val(DefectsNum.Text)
            mybug = bfact.AddItem(DBNull.Value)
            mybug.Summary = "Defect" & i & " " & LoginTime
            Dim mydate = Now.Date.ToString.Substring(0, 10)
            mybug.Field("BG_DETECTION_DATE") = mydate
            mybug.Status = "New"
            If i <= Critical Then
                mybug.Priority = "5-Urgent"
                mybug.Field("BG_SEVERITY") = "5-Urgent"
            ElseIf i <= veryHigh Then
                mybug.Priority = "4-Very High"
                mybug.Field("BG_SEVERITY") = "4-Very High"
            ElseIf i <= high Then
                mybug.Priority = "3-High"
                mybug.Field("BG_SEVERITY") = "3-High"
            ElseIf i <= meduim Then
                mybug.Priority = "2-Medium"
                mybug.Field("BG_SEVERITY") = "2-Medium"
            Else
                mybug.Priority = "1-Low"
                mybug.Field("BG_SEVERITY") = "1-Low"
            End If
            mybug.DetectedBy = Username.Text
            mybug.Post()
            DefectsList.Add(mybug)
            AddEventToLog(1, "Defect " & mybug.ID & " was added")
            'Handle attachement in defect
            If AttachmentCheckBox.Enabled Then
                If i <= Val(DefectAttachment.Text) Then
                    Dim attachF As TDAPIOLELib.AttachmentFactory = mybug.Attachments
                    Dim attachment As TDAPIOLELib.Attachment = attachF.AddItem(DBNull.Value)
                    attachment.FileName = AttachementTextBox.Text
                    attachment.Type = TDAPIOLELib.TDAPI_ATTACH_TYPE.TDATT_FILE
                    attachment.Post()
                    AddEventToLog(1, "Attachement was added to defect " & mybug.ID)
                End If
            End If
        Next i

        'Handle statuses
        Dim open, fixed, closed, rejected, reopen As Integer
        open = (Val(DefectsNum.Text) * Val(OpenTextBox.Text)) / 100
        fixed = (Val(DefectsNum.Text) * Val(FixTextBox.Text)) / 100 + open
        closed = (Val(DefectsNum.Text) * Val(CloseTextBox.Text)) / 100 + fixed
        rejected = (Val(DefectsNum.Text) * Val(RejectTextBox.Text)) / 100 + closed
        reopen = (Val(DefectsNum.Text) * Val(ReopenTextBox.Text)) / 100 + rejected


        For i = 1 To DefectsList.Count
            mybug = DefectsList.Item(i)
            If i <= open Then
                mybug.Status = "Open"
            ElseIf i <= fixed Then
                mybug.Status = "Fixed"
            ElseIf i <= closed Then
                mybug.Status = "Closed"
            ElseIf i <= rejected Then
                mybug.Status = "Rejected"
            ElseIf i <= reopen Then
                mybug.Status = "Reopen"
            Else
                Exit For
            End If
            mybug.Post()
        Next
    End Sub


    Private Sub Handle_Defects_Relations()

        Dim bfact As TDAPIOLELib.BugFactory
        bfact = tdc.BugFactory


        Dim ListdefectsNum As Long
        ListdefectsNum = DefectsList.Count

        Dim NumOfMaxLinks = Int(((ListdefectsNum - 1) * ListdefectsNum) / 2)
        If (Val(DefectDefectLNK.Text)) >= NumOfMaxLinks Then
            Throw New Exception("Number of Links between defects should be Less than the number choices of (number of defects select 2)." & vbCrLf & "Refresh your combinatorics")
        End If

        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link
        Dim ilink1 As TDAPIOLELib.ILinkable


        Dim numDefectsToLink As Long
        numDefectsToLink = Val(DefectDefectLNK.Text)
        Dim Rand As Long
        Dim defect1, defect2 As TDAPIOLELib.Bug

        For i = 1 To numDefectsToLink
            Randomize()
            Rand = Int((ListdefectsNum - 1 + 1) * Rnd()) + 1
            defect1 = DefectsList.Item(Rand)
            ' Don't allow link between defect and itself
            Do
                Randomize()
                Rand = Int((ListdefectsNum - 1 + 1) * Rnd()) + 1
                defect2 = DefectsList.Item(Rand)
            Loop While defect1.ID = defect2.ID

            ilink1 = defect1
            linkF = ilink1.BugLinkFactory
            link = linkF.AddItem(defect2)
            Try
                AddEventToLog(1, "Linking " & defect1.ID.ToString() & " and defect " & defect2.ID.ToString())
                link.Post()
            Catch exp As Exception
                If exp.Message.Contains("already linked") Then
                    i -= 1
                    AddEventToLog(1, "Duplication Found: Randomly chosen entities are already linked , trying again...")
                Else : Throw exp
                End If
            End Try
        Next

    End Sub


    Private Sub Handle_Requirements()
        Dim reqF As TDAPIOLELib.ReqFactory
        reqF = tdc.ReqFactory
        Dim newR As TDAPIOLELib.Req

        Dim Reqcounter = 0
        Dim list1 As Queue(Of Long) = New Queue(Of Long)
        list1.Clear()

        list1.Enqueue(0) 'the first requirement in the queue is req.id=0

        For j = 1 To Val(ReqLevels.Text) 'In every level do the following this
            Dim list2 As Queue(Of Long) = New Queue(Of Long)
            list2.Clear()

            For k = 1 To list1.Count 'for all the reqs in the list
                Dim FatherReqID As Long
                FatherReqID = list1.Dequeue
                For i = 1 To Val(ReqsNum.Text) 'create reqnum elements

                    Reqcounter += 1
                    newR = reqF.AddItem(System.DBNull.Value)

                    With newR
                        .ParentId = FatherReqID
                        .Name = "Sub Req " & i & " of req " & FatherReqID.ToString
                        If FatherReqID = 0 Then
                            .Name &= " at " & LoginTime.Replace("/", "-").Replace(":", "-")
                        End If
                        .Comment = "Sub Req " & i & " of req " & FatherReqID.ToString
                        .Post()
                        AddEventToLog(1, "Creating requirement " & newR.ID)
                        ReqsList.Add(newR)
                        list2.Enqueue(newR.ID) 'Insert the created requirement in the queue

                        If AttachmentCheckBox.Checked Then
                            If Reqcounter <= Val(ReqAttachment.Text) Then
                                Dim AttachF As TDAPIOLELib.AttachmentFactory = newR.Attachments
                                Dim attachment As TDAPIOLELib.Attachment = AttachF.AddItem(DBNull.Value)
                                attachment.FileName = AttachementTextBox.Text
                                attachment.Type = TDAPIOLELib.TDAPI_ATTACH_TYPE.TDATT_FILE
                                AddEventToLog(1, "Adding Attachmnet for Requirment " & newR.ID)
                                attachment.Post()
                            End If
                        End If
                    End With
                Next
            Next
            list1 = list2
        Next

    End Sub

    Private Sub Handle_req_traceability()
        Dim reqF As TDAPIOLELib.ReqFactory
        reqF = tdc.ReqFactory
        For i = 1 To Val(ReqReqLNK.Text)
            Dim rand = GetRandomInt(1, ReqsList.Count)
            Dim req As TDAPIOLELib.Req
            Dim traceF As TDAPIOLELib.ReqTraceFactory
            req = ReqsList(rand)
            traceF = req.ReqTraceFactory(1) '1 = trace to
            Dim randto = GetRandomInt(1, ReqsList.Count)
            Try
                Dim req2 = ReqsList(randto)
                traceF.AddItem(req2)
                req.Post()
                AddEventToLog(1, "Linking requirment " & req.ID & " to requiremnt " & req2.ID)
            Catch ex As Exception
                If ex.Message.Contains("already exist") Then
                    i -= 1
                    AddEventToLog(1, "Duplication Found: Randomly chosen entities are already linked , trying again...")
                Else : Throw ex
                End If
            End Try
        Next
    End Sub


    Public Sub Handle_Tests()

        Dim test1 As TDAPIOLELib.Test
        Dim testF As TDAPIOLELib.TestFactory
        Dim TestCounter = 0

        Dim treeMng As TDAPIOLELib.TreeManager
        Dim oRoot, folder As TDAPIOLELib.SubjectNode
        treeMng = tdc.TreeManager
        oRoot = treeMng.TreeRoot("Subject")

        Dim list1 As Queue(Of TDAPIOLELib.SubjectNode) = New Queue(Of TDAPIOLELib.SubjectNode)
        list1.Clear()
        list1.Enqueue(oRoot)
        For j = 1 To Val(TestDirLevels.Text)  'for each level do
            Dim list2 As Queue(Of TDAPIOLELib.SubjectNode) = New Queue(Of TDAPIOLELib.SubjectNode)
            list2.Clear()
            For d = 1 To list1.Count          ' for each folder in list1

                Dim currfold As TDAPIOLELib.SubjectNode
                currfold = list1.Dequeue()    ' remove the folder from list1

                For i = 1 To Val(TestDirsInLevel.Text)
                    If currfold.NodeID = oRoot.NodeID Then
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of Folder " & currfold.NodeID.ToString & " at " & LoginTime)
                    Else
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of Folder " & currfold.NodeID.ToString)
                    End If
                    folder.Post()
                    AddEventToLog(1, "Creating Test Folder " & folder.Name)
                    list2.Enqueue(folder)
                    For k = 1 To Val(TestsInLevel.Text)
                        testF = folder.TestFactory
                        test1 = testF.AddItem(System.DBNull.Value)
                        test1.Name = "Test " & k.ToString
                        test1.Post()
                        TestsList.Add(test1)
                        AddEventToLog(1, "Creating Test " & test1.ID)
                        'Handle Steps
                        For s = 1 To Val(StepsTextBox.Text)
                            Dim Designstep As TDAPIOLELib.DesignStep
                            Dim DesignStepF As TDAPIOLELib.DesignStepFactory
                            DesignStepF = test1.DesignStepFactory
                            Designstep = DesignStepF.AddItem(DBNull.Value)
                            Designstep.StepName = "Step " & s
                            Designstep.StepDescription = "Step " & s & "of Test " & test1.ID.ToString
                            Designstep.StepExpectedResult = "Expected Result of step " & s
                            Designstep.Post()
                        Next
                        'Handle Attachments
                        TestCounter += 1
                        If AttachmentCheckBox.Checked Then
                            If TestCounter <= Val(TestPlanAttachement.Text) Then
                                Dim attachF As TDAPIOLELib.AttachmentFactory = test1.Attachments
                                Dim attachment As TDAPIOLELib.Attachment = attachF.AddItem(DBNull.Value)
                                attachment.FileName = AttachementTextBox.Text
                                attachment.Type = TDAPIOLELib.TDAPI_ATTACH_TYPE.TDATT_FILE
                                attachment.Post()
                                AddEventToLog(1, "Adding Attachment for Test " & test1.ID)
                            End If
                        End If
                    Next
                Next
            Next
            list1 = list2
        Next

    End Sub

    Private Sub Handle_Test_Sets()

        Dim treeMng As TDAPIOLELib.TestSetTreeManager
        Dim oRoot, folder As TDAPIOLELib.TestSetFolder

        treeMng = tdc.TestSetTreeManager
        oRoot = treeMng.Root

        Dim TestSetCounter = 0

        Dim list1 As Queue(Of TDAPIOLELib.TestSetFolder) = New Queue(Of TDAPIOLELib.TestSetFolder)
        list1.Clear()
        list1.Enqueue(oRoot)
        For j = 1 To Val(SetDirsLevels.Text)  'for each level do
            Dim list2 As Queue(Of TDAPIOLELib.TestSetFolder) = New Queue(Of TDAPIOLELib.TestSetFolder)
            list2.Clear()
            For d = 1 To list1.Count          ' for each folder in list1

                Dim currfold As TDAPIOLELib.TestSetFolder
                currfold = list1.Dequeue()    ' remove the folder from list1

                For i = 1 To Val(SetDirsInLevel.Text)
                    If currfold.NodeID = oRoot.NodeID Then
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of " & currfold.Name & " at " & LoginTime)
                    Else
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of " & currfold.NodeID.ToString)
                    End If
                    AddEventToLog(1, "Creating Folder " & folder.Name)
                    folder.Post()
                    list2.Enqueue(folder)
                    If folder.Path <> oRoot.Path Then

                        For k = 1 To Val(SetsinDir.Text)
                            Dim Tset As TDAPIOLELib.TestSet
                            Dim setF As TDAPIOLELib.TestSetFactory
                            setF = folder.TestSetFactory
                            Tset = setF.AddItem(System.DBNull.Value)
                            Tset.Name = "Test Set " & k.ToString
                            Tset.Post()
                            AddEventToLog(1, "Creating Test Set " & Tset.ID)
                            TestSetsList.Add(Tset)
                            'Handle attachement in test sets
                            TestSetCounter += 1
                            If AttachmentCheckBox.Checked Then
                                If TestSetCounter <= Val(TestLabAttachment.Text) Then
                                    Dim attachF As TDAPIOLELib.AttachmentFactory = Tset.Attachments
                                    Dim attachment As TDAPIOLELib.Attachment = attachF.AddItem(DBNull.Value)
                                    attachment.FileName = AttachementTextBox.Text
                                    attachment.Type = TDAPIOLELib.TDAPI_ATTACH_TYPE.TDATT_FILE
                                    AddEventToLog(1, "Adding Attachement for Test Set " & Tset.ID)
                                    attachment.Post()
                                End If
                            End If
                            If TestCheckBox.Checked Then
                                Dim list As TDAPIOLELib.List = New TDAPIOLELib.List
                                Dim TestF As TDAPIOLELib.TestFactory
                                TestF = tdc.TestFactory
                                list = TestF.NewList("")
                                For N = 1 To Val(TestInstInSet.Text)
                                    Dim Rand = GetRandomInt(1, list.Count)
                                    Dim test As TDAPIOLELib.Test
                                    test = list.Item(Rand)
                                    Dim TestInsF As TDAPIOLELib.TSTestFactory
                                    TestInsF = Tset.TSTestFactory
                                    TestInsF.AddItem(test.ID)
                                    Tset.Post()
                                Next
                            End If
                        Next
                    End If
                Next
            Next
            list1 = list2
        Next
    End Sub

    Private Sub Handle_Req_test_coverage()

        Dim req As TDAPIOLELib.Req
        Dim Test As TDAPIOLELib.Test
        Dim rand As Integer
        Dim coverable As TDAPIOLELib.ICoverableReq



        For i = 1 To Val(TestReqLNK.Text)

            'rand = Int((High - Low + 1) * Rnd()) + Low
            Do
                Randomize()
                rand = Int((TestsList.Count - 1 + 1) * Rnd()) + 1
                Test = TestsList.Item(rand)
            Loop While Test.ID = 0

            Do
                Randomize()
                rand = Int((ReqsList.Count - 1 + 1) * Rnd()) + 1
                req = ReqsList.Item(rand)
            Loop While req.ID = 0

            coverable = req
            Dim reqid = req.ID
            Dim testid = Test.ID
            Dim DupCovflag = False

            'make sure not to re-cover the requirement with the same test or one of it's sons 
            Dim CoveringTests As TDAPIOLELib.List = req.GetCoverList(True)

            For Each tempTest In CoveringTests
                If tempTest.ID = Test.ID Then
                    i = i - 1
                    DupCovflag = True
                    AddEventToLog(1, "Duplication Found: Randomly chosen entities are already linked , trying again...")
                    Exit For
                End If
            Next
            If DupCovflag = False Then
                coverable.AddTestToCoverage(Test.ID)
                AddEventToLog(1, "Covering Requirment " & req.ID & " by Test " & Test.ID)
            End If
        Next

    End Sub

    Private Sub Handle_Requirements_defects_linkage()


        Dim defectsNum As Long
        Dim Rand As Long
        Dim defect As TDAPIOLELib.Bug

        Dim req As TDAPIOLELib.Req
        Dim reqNum As Long


        defectsNum = DefectsList.Count

        reqNum = ReqsList.Count

        Dim ilink As TDAPIOLELib.ILinkable
        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link

        For i = 1 To Val(ReqDefectLNK.Text)
            Randomize()
            Rand = Int((defectsNum - 1 + 1) * Rnd()) + 1
            defect = DefectsList.Item(Rand)

            Randomize()
            Rand = Int((reqNum - 1 + 1) * Rnd()) + 1
            req = ReqsList.Item(Rand)
            Try
                ilink = req
                linkF = ilink.BugLinkFactory
                link = linkF.AddItem(defect)
                AddEventToLog(1, "Linking requirment " & req.ID & " and Defect " & defect.ID)
                link.Post()
            Catch ex As Exception
                If ex.Message.Contains("already linked") Then
                    i -= 1
                    AddEventToLog(1, "Duplication Found: Randomly chosen entities are already linked , trying again...")
                Else : Throw ex
                End If
            End Try

        Next
    End Sub

    Private Sub Handle_Tests_defects_linkage()


        Dim defectsNum As Long
        Dim Rand As Long
        Dim defect As TDAPIOLELib.Bug
        Dim test As TDAPIOLELib.Test
        Dim testNum As Long

        defectsNum = DefectsList.Count
        testNum = TestsList.Count

        Dim ilink As TDAPIOLELib.ILinkable
        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link

        For i = 1 To Val(TestDefectLNK.Text)
            Randomize()
            Rand = Int((defectsNum - 1 + 1) * Rnd()) + 1
            defect = DefectsList.Item(Rand)

            Randomize()
            Rand = Int((testNum - 1 + 1) * Rnd()) + 1
            test = TestsList.Item(Rand)

            ilink = test
            linkF = ilink.BugLinkFactory
            link = linkF.AddItem(defect)

            Try
                link.Post()
                AddEventToLog(1, "Linking Test " & test.ID & " and Defect " & defect.ID)
            Catch exp As Exception
                i = i - 1
                AddEventToLog(1, "Duplication Found: Randomly chosen entities are already linked , trying again...")
                Continue For
            End Try

        Next
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

    Private Function GetRandomInt(ByVal Low, ByVal High) As Integer
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

    Private Sub InitializeLogFile()
        'Create the log file in the filesystem
        Dim ActualLogFileName As String
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
        LogFile.WriteLine("<tr> <th>Time</th><th>Memory Usage</th><th>Category</th><th>Message</th></tr>")

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

    Private Sub AddEventToLog(ByVal Category As Integer, ByVal Message As String)
        If LoggerCheckBox.Checked Then
            LogFile.WriteLine("<tr>")
            Dim NowTime = Now.TimeOfDay.ToString()
            If NowTime.Length >= 12 Then
                NowTime = NowTime.Substring(0, 12)
            End If
            Dim NowDay = Now.Date.ToString.Substring(0, 10)
            LogFile.WriteLine("<td>" & NowDay & "  " & NowTime & "</td>")
            LogFile.WriteLine("<td>" & Int(GetMemoryUsage() / 1024) & " MB" & "</td>")
            If Category = 1 Then
                LogFile.WriteLine("<td title=""Category"">" & "Info" & "</td>")
            Else
                LogFile.WriteLine("<td title=""Category"">" & "Performance" & "</td>")
            End If
            LogFile.WriteLine("<td title=""Message"">" & Message & "</td>")
            LogFile.WriteLine("</tr>")
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


