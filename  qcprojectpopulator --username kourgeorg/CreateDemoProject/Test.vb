Module Test
    Public TestsList As TDAPIOLELib.List

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
        For j = 1 To Val(Form.TestDirLevels.Text)  'for each level do
            Dim list2 As Queue(Of TDAPIOLELib.SubjectNode) = New Queue(Of TDAPIOLELib.SubjectNode)
            list2.Clear()
            For d = 1 To list1.Count          ' for each folder in list1

                Dim currfold As TDAPIOLELib.SubjectNode
                currfold = list1.Dequeue()    ' remove the folder from list1

                For i = 1 To Val(Form.TestDirsInLevel.Text)
                    If currfold.NodeID = oRoot.NodeID Then
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of Folder " & currfold.NodeID.ToString & " at " & LoginTime)
                    Else
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of Folder " & currfold.NodeID.ToString)
                    End If
                    folder.Post()
                    AddEventToLog(1, "Creating Test Folder " & folder.Name)
                    list2.Enqueue(folder)
                    For k = 1 To Val(Form.TestsInLevel.Text)
                        testF = folder.TestFactory
                        test1 = testF.AddItem(System.DBNull.Value)
                        test1.Name = "Test " & k.ToString
                        test1.Post()
                        TestsList.Add(test1)
                        AddEventToLog(1, "Creating Test " & test1.ID)
                        'Handle Steps
                        For s = 1 To Val(Form.StepsTextBox.Text)
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
                        If Form.AttachmentCheckBox.Checked Then
                            If TestCounter <= Val(Form.TestPlanAttachement.Text) Then
                                Dim attachF As TDAPIOLELib.AttachmentFactory = test1.Attachments
                                Dim attachment As TDAPIOLELib.Attachment = attachF.AddItem(DBNull.Value)
                                attachment.FileName = Form.AttachementTextBox.Text
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

    Public Sub Handle_Tests_defects_linkage()
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

        Dim NumOfMaxLinks = Int(((defectsNum) * testNum))
        If (Val(Form.TestDefectLNK.Text)) > NumOfMaxLinks Then
            Throw New Exception("Given " & defectsNum.ToString & " Defects and " & testNum.ToString & " Test, Number of max Linkage is " & NumOfMaxLinks.ToString & vbCrLf & "Refresh your combinatorics")
        End If

        For i = 1 To Val(Form.TestDefectLNK.Text)
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
End Module
