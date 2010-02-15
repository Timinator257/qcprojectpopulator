Imports System.Net.NetworkInformation
Imports System.IO

Public Class Form1
    Dim tdc As TDAPIOLELib.TDConnection
    Dim DomainsList As List(Of String)
    Dim ProjectsLists As List(Of List(Of String))
    Dim LoginTime As String

    Private Sub SetLoginTime()
        LoginTime = Now.Date.ToString.Substring(0, 9).Replace("/", "-") & " " & Now.TimeOfDay.ToString.Replace(":", "-")
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Populate.Click

        SetLoginTime()
        ProgressBar.Value = 0
        Try
            Connect()
            ProgressBar.Increment(10)
            Result.Text = "Login Completed Successfully"
            Refresh()
        Catch ex As Exception
            Result.Text = "Can't connect to Server"
            Return
        End Try

        Try

            'Handle Defects
            If DefectsCheckBox.Checked = True Then
                Handle_Defects()
                Handle_Defects_Relations()

                Result.Text = "Creating Defects Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If



            'Handle Requirements
            If ReqCheckBox.Checked = True Then
                Handle_Requirements()
                Handle_req_tracebility()

                Result.Text = "Creating Requirements Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If




            'Handle Tests
            If TestCheckBox.Checked = True Then
                Handle_Tests()
                Handle_Steps_In_Test()

                Result.Text = "Handle Tests Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If

            'Handle Test Lab
            If TestLabCheckBox.Checked Then
                Handle_Test_Sets()

                Result.Text = "Handle Test Sets Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If



            'Handles req links to defects
            If LinkCheckBox.Checked = True And ReqCheckBox.Checked = True And DefectsCheckBox.Checked = True Then
                Handle_Requirements_defects_linkage()

                Result.Text = "Handle Requirements-defects linkage Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If



            'Handle tests links to defects
            If LinkCheckBox.Checked = True And TestCheckBox.Checked = True And DefectsCheckBox.Checked = True Then
                Handle_Tests_defects_linkage()

                Result.Text = "Handle Tests-defects linkage Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If



            'Handle Req-test coverage
            If ReqCheckBox.Checked And TestCheckBox.Checked Then
                Handle_Req_test_coverage()

                Result.Text = "Handle Tests-Requirements linkage Completed Successfully"
                ProgressBar.Increment(10)
                Refresh()
            End If

            Result.Text = "Completed Successfully"


        Catch exp As Exception
            Result.Text = exp.Message

        Finally
            ProgressBar.Value = 100
            Refresh()

        End Try

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tdc = New TDAPIOLELib.TDConnection
    End Sub
    Private Sub Login()

        tdc.InitConnectionEx(ServerURL.Text)
        If (tdc.Connected = True) Then
            tdc.Login(Username.Text, Password.Text)
        End If

    End Sub
    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click

        Try
            Result.Text = ""
            ProgressBar.Value = 0
            Disconnect()
            Login()
            Populate_Login_lists()
            Populate_Domain_ComboBox()


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

        If tdc.Connected Then
            If tdc.LoggedIn = True Then
                tdc.Logout()
            End If
            tdc.Disconnect()
        End If

        tdc.ReleaseConnection()

    End Sub

    Private Sub Handle_req_tracebility()
        Dim reqF As TDAPIOLELib.ReqFactory
        reqF = tdc.ReqFactory
        Dim reqList = reqF.NewList("")
        For i = 1 To Val(TracebilityTextBox.Text)

            Dim rand = GetRandomInt(1, reqList.Count)
            Dim req As TDAPIOLELib.Req
            Dim traceF As TDAPIOLELib.ReqTraceFactory
            req = reqList(rand)
            traceF = req.ReqTraceFactory(1) '1 = trace to
            Dim randto = GetRandomInt(1, reqList.Count)
            Try
                traceF.AddItem(reqList(randto))
                req.Post()
            Catch ex As Exception
                i -= 1
            End Try

        Next
    End Sub


    Private Sub Handle_Defects()

        Dim bfact As TDAPIOLELib.BugFactory
        bfact = tdc.BugFactory
        Dim mybug As TDAPIOLELib.Bug
        Dim high, meduim, low, veryHigh, Critical As Integer
        Critical = Val((DefectsNum.Text) * (CriticalTextBox.Text)) / 100
        veryHigh = Val((DefectsNum.Text) * (VeryHighTextBox.Text)) / 100 + Critical
        high = Val((DefectsNum.Text) * (HighTextBox.Text)) / 100 + veryHigh
        meduim = Val((DefectsNum.Text) * (MeduimTextBox.Text)) / 100 + high
        low = Val((DefectsNum.Text) * (LowTextBox.Text)) / 100 + meduim

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
        Next i

        'Handle statuses
        Dim open, fixed, closed, rejected, reopen As Integer
        open = Val((DefectsNum.Text) * (OpenTextBox.Text)) / 100
        fixed = Val((DefectsNum.Text) * (FixTextBox.Text)) / 100 + open
        closed = Val((DefectsNum.Text) * (CloseTextBox.Text)) / 100 + fixed
        rejected = Val((DefectsNum.Text) * (RejectTextBox.Text)) / 100 + closed
        reopen = Val((DefectsNum.Text) * (ReopenTextBox.Text)) / 100 + rejected

        Dim BugList As TDAPIOLELib.List
        BugList = bfact.NewList("")

        For i = 1 To BugList.Count
            mybug = BugList.Item(i)
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

        If (Val(LinksBetweenDefects.Text) >= Val(DefectsNum.Text)) Then
            Throw New Exception("Number of Links between defects should be greater that the defects number")
        End If

        Dim bfact As TDAPIOLELib.BugFactory
        bfact = tdc.BugFactory


        Dim DefectList As TDAPIOLELib.IList
        DefectList = bfact.NewList("")
        Dim ListdefectsNum As Long
        ListdefectsNum = DefectList.Count

        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link
        Dim ilink1 As TDAPIOLELib.ILinkable


        Dim numDefectsToLink As Long
        numDefectsToLink = Val(LinksBetweenDefects.Text)
        Dim Rand As Long
        Dim defect1, defect2 As TDAPIOLELib.Bug

        For i = 1 To numDefectsToLink
            Randomize()
            Rand = Int((ListdefectsNum - 1 + 1) * Rnd()) + 1
            defect1 = DefectList.Item(Rand)
            ' don't allow link between defect and itself
            Do
                Randomize()
                Rand = Int((ListdefectsNum - 1 + 1) * Rnd()) + 1
                defect2 = DefectList.Item(Rand)
            Loop While defect1.ID = defect2.ID

            ilink1 = defect1
            linkF = ilink1.BugLinkFactory
            link = linkF.AddItem(defect2)
            link.Post()
        Next

    End Sub


    Private Sub Handle_Requirements()
        Dim reqF As TDAPIOLELib.ReqFactory
        reqF = tdc.ReqFactory
        Dim newR As TDAPIOLELib.Req

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
                    newR = reqF.AddItem(System.DBNull.Value)

                    With newR
                        .ParentId = FatherReqID
                        .Name = "Sub Req " & i & " of req " & FatherReqID.ToString & " at " & LoginTime
                        .Comment = "Sub Req " & i & " of req " & FatherReqID.ToString & " at " & LoginTime & " Comment"
                        .Post()
                        list2.Enqueue(newR.ID) 'Insert the ceated requirement in the queue
                    End With
                Next
            Next
            list1 = list2
        Next

    End Sub

    Public Sub Handle_Tests()

        Dim test1 As TDAPIOLELib.Test
        Dim testF As TDAPIOLELib.TestFactory


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
                    folder = currfold.AddNode("Subfolder " & i.ToString & " of " & currfold.Name.ToString & " " & LoginTime)
                    folder.Post()
                    list2.Enqueue(folder)
                    For k = 1 To Val(TestsInLevel.Text)
                        testF = folder.TestFactory
                        test1 = testF.AddItem(System.DBNull.Value)
                        test1.Name = "Test " & k.ToString
                        test1.Post()
                    Next
                Next
            Next
            list1 = list2
        Next

    End Sub
    Private Sub Handle_Steps_In_Test()
        Dim TestF As TDAPIOLELib.TestFactory
        Dim TestList As TDAPIOLELib.List
        TestF = tdc.TestFactory
        TestList = TestF.NewList("")
        For i = 1 To TestList.Count
            Dim test As TDAPIOLELib.Test
            test = TestList.Item(i)
            For j = 1 To Val(StepsTextBox.Text)
                Dim Designstep As TDAPIOLELib.DesignStep
                Dim DesignStepF As TDAPIOLELib.DesignStepFactory
                DesignStepF = test.DesignStepFactory
                Designstep = DesignStepF.AddItem(DBNull.Value)
                Designstep.StepName = "Step " & j
                Designstep.StepDescription = "Step " & j & "of Test " & test.ID.ToString
                Designstep.StepExpectedResult = "Expected Result of step " & j
                Designstep.Post()
            Next
        Next
    End Sub

    Private Sub Handle_Test_Sets()



        Dim treeMng As TDAPIOLELib.TestSetTreeManager
        Dim oRoot, folder As TDAPIOLELib.TestSetFolder

        treeMng = tdc.TestSetTreeManager
        oRoot = treeMng.Root

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
                    folder = currfold.AddNode("Subfolder " & i.ToString & " of " & currfold.Name.ToString & " " & LoginTime)
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
                            If TestCheckBox.Checked Then
                                Dim list As TDAPIOLELib.List
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
        Dim ReqF As TDAPIOLELib.ReqFactory
        Dim ReqList As TDAPIOLELib.List
        Dim req As TDAPIOLELib.Req

        Dim Test As TDAPIOLELib.Test
        Dim TestF As TDAPIOLELib.TestFactory
        Dim TestList As TDAPIOLELib.List

        Dim rand As Integer

        Dim coverable As TDAPIOLELib.ICoverableReq

        ReqF = tdc.ReqFactory
        ReqList = ReqF.NewList("")

        TestF = tdc.TestFactory
        TestList = TestF.NewList("")


        For i = 1 To Val(TestCoverageTextBox.Text)

            'rand = Int((High - Low + 1) * Rnd()) + Low
            Do
                Randomize()
                rand = Int((TestList.Count - 1 + 1) * Rnd()) + 1
                Test = TestList.Item(rand)
            Loop While Test.ID = 0

            Do
                Randomize()
                rand = Int((ReqList.Count - 1 + 1) * Rnd()) + 1
                req = ReqList.Item(rand)
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
                    Exit For
                End If
            Next
            If DupCovflag = False Then
                coverable.AddTestToCoverage(Test.ID)
            End If
        Next

    End Sub

    Private Sub Handle_Requirements_defects_linkage()

        Dim bfact As TDAPIOLELib.BugFactory
        Dim DefectList As TDAPIOLELib.IList
        Dim defectsNum As Long
        Dim Rand As Long
        Dim defect As TDAPIOLELib.Bug

        Dim rfact As TDAPIOLELib.ReqFactory
        Dim req As TDAPIOLELib.Req
        Dim ReqList As TDAPIOLELib.IList
        Dim reqNum As Long

        bfact = tdc.BugFactory
        DefectList = bfact.NewList("")
        defectsNum = DefectList.Count

        rfact = tdc.ReqFactory
        ReqList = rfact.NewList("")
        reqNum = ReqList.Count

        Dim ilink As TDAPIOLELib.ILinkable
        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link

        For i = 1 To Val(ReqDefectlnk.Text)
            Randomize()
            Rand = Int((defectsNum - 1 + 1) * Rnd()) + 1
            defect = DefectList.Item(Rand)

            Randomize()
            Rand = Int((reqNum - 1 + 1) * Rnd()) + 1
            req = ReqList.Item(Rand)

            ilink = req
            linkF = ilink.BugLinkFactory
            link = linkF.AddItem(defect)
            link.Post()
        Next
    End Sub

    Private Sub Handle_Tests_defects_linkage()

        Dim bfact As TDAPIOLELib.BugFactory
        Dim DefectList As TDAPIOLELib.IList
        Dim defectsNum As Long
        Dim Rand As Long
        Dim defect As TDAPIOLELib.Bug

        Dim testfact As TDAPIOLELib.TestFactory
        Dim test As TDAPIOLELib.Test
        Dim TestList As TDAPIOLELib.IList
        Dim testNum As Long

        bfact = tdc.BugFactory
        DefectList = bfact.NewList("")
        defectsNum = DefectList.Count

        testfact = tdc.TestFactory
        TestList = testfact.NewList("")
        testNum = TestList.Count

        Dim ilink As TDAPIOLELib.ILinkable
        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link

        For i = 1 To Val(TestDefectLnk.Text)
            Randomize()
            Rand = Int((defectsNum - 1 + 1) * Rnd()) + 1
            defect = DefectList.Item(Rand)

            Randomize()
            Rand = Int((testNum - 1 + 1) * Rnd()) + 1
            test = TestList.Item(Rand)

            ilink = test
            linkF = ilink.BugLinkFactory
            link = linkF.AddItem(defect)

            Try
                link.Post()
            Catch exp As Exception
                i = i - 1
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
            MessageBox.Show("Illigal Distribution")
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
            MessageBox.Show("Illigal Distribution")
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


End Class


