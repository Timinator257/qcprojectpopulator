Module TestSet
    Public TestSetsList As TDAPIOLELib.List

    Public Sub Handle_Test_Sets()

        Dim treeMng As TDAPIOLELib.TestSetTreeManager
        Dim oRoot, folder As TDAPIOLELib.TestSetFolder

        treeMng = tdc.TestSetTreeManager
        oRoot = treeMng.Root

        Dim TestSetCounter = 0

        Dim list1 As Queue(Of TDAPIOLELib.TestSetFolder) = New Queue(Of TDAPIOLELib.TestSetFolder)
        list1.Clear()
        list1.Enqueue(oRoot)
        For j = 1 To Val(Form.SetDirsLevels.Text)  'for each level do
            Dim list2 As Queue(Of TDAPIOLELib.TestSetFolder) = New Queue(Of TDAPIOLELib.TestSetFolder)
            list2.Clear()
            For d = 1 To list1.Count          ' for each folder in list1

                Dim currfold As TDAPIOLELib.TestSetFolder
                currfold = list1.Dequeue()    ' remove the folder from list1

                For i = 1 To Val(Form.SetDirsInLevel.Text)
                    If currfold.NodeID = oRoot.NodeID Then
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of " & currfold.Name & " at " & LoginTime)
                    Else
                        folder = currfold.AddNode("Subfolder " & i.ToString & " of " & currfold.NodeID.ToString)
                    End If
                    AddEventToLog(1, "Creating Folder " & folder.Name)
                    folder.Post()
                    list2.Enqueue(folder)
                    If folder.Path <> oRoot.Path Then

                        For k = 1 To Val(Form.SetsinDir.Text)
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
                            If Form.AttachmentCheckBox.Checked Then
                                If TestSetCounter <= Val(Form.TestLabAttachment.Text) Then
                                    Dim attachF As TDAPIOLELib.AttachmentFactory = Tset.Attachments
                                    Dim attachment As TDAPIOLELib.Attachment = attachF.AddItem(DBNull.Value)
                                    attachment.FileName = Form.AttachementTextBox.Text
                                    attachment.Type = TDAPIOLELib.TDAPI_ATTACH_TYPE.TDATT_FILE
                                    AddEventToLog(1, "Adding Attachement for Test Set " & Tset.ID)
                                    attachment.Post()
                                End If
                            End If
                            If Form.TestCheckBox.Checked Then
                                Dim list As TDAPIOLELib.List = New TDAPIOLELib.List
                                Dim TestF As TDAPIOLELib.TestFactory
                                TestF = tdc.TestFactory
                                list = TestF.NewList("")
                                For N = 1 To Val(Form.TestInstInSet.Text)
                                    Dim Rand = Form.GetRandomInt(1, list.Count)
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
End Module
