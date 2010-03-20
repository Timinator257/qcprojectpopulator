Module Requirement
    Public ReqsList As TDAPIOLELib.List

    Public Sub Handle_Requirements()
        Dim reqF As TDAPIOLELib.ReqFactory
        reqF = tdc.ReqFactory
        Dim newR As TDAPIOLELib.Req

        Dim Reqcounter = 0
        Dim list1 As Queue(Of Long) = New Queue(Of Long)
        list1.Clear()

        list1.Enqueue(0) 'the first requirement in the queue is req.id=0

        For j = 1 To Val(Form.ReqLevels.Text) 'In every level do the following this
            Dim list2 As Queue(Of Long) = New Queue(Of Long)
            list2.Clear()

            For k = 1 To list1.Count 'for all the reqs in the list
                Dim FatherReqID As Long
                FatherReqID = list1.Dequeue
                For i = 1 To Val(Form.ReqsNum.Text) 'create reqnum elements

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

                        If Form.AttachmentCheckBox.Checked Then
                            If Reqcounter <= Val(Form.ReqAttachment.Text) Then
                                Dim AttachF As TDAPIOLELib.AttachmentFactory = newR.Attachments
                                Dim attachment As TDAPIOLELib.Attachment = AttachF.AddItem(DBNull.Value)
                                attachment.FileName = Form.AttachementTextBox.Text
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

    Public Sub Handle_req_traceability()
        Dim reqF As TDAPIOLELib.ReqFactory
        reqF = tdc.ReqFactory

        Dim ListreqsNum As Long
        ListreqsNum = ReqsList.Count

        Dim NumOfMaxLinks = Int(((ListreqsNum - 1) * ListreqsNum))
        If (Val(Form.ReqReqLNK.Text)) > NumOfMaxLinks Then
            Throw New Exception("Given " & ListreqsNum.ToString & " Requirements, Number of max traces between requirements is " & NumOfMaxLinks.ToString & vbCrLf & "Refresh your combinatorics")
        End If

        For i = 1 To Val(Form.ReqReqLNK.Text)
            Dim rand = Form.GetRandomInt(1, ReqsList.Count)
            Dim req As TDAPIOLELib.Req
            Dim traceF As TDAPIOLELib.ReqTraceFactory
            req = ReqsList(rand)
            traceF = req.ReqTraceFactory(1) '1 = trace to
            Dim randto = Form.GetRandomInt(1, ReqsList.Count)
            Try
                Dim req2 = ReqsList(randto)
                traceF.AddItem(req2)
                req.Post()
                AddEventToLog(1, "Linking requirment " & req.ID & " to requiremnt " & req2.ID)
            Catch ex As Exception
                If ex.Message.Contains("already traced") Then
                    i -= 1
                    AddEventToLog(1, "Duplication Found: Randomly chosen entities are already linked , trying again...")
                Else : Throw ex
                End If
            End Try
        Next
    End Sub


    Public Sub Handle_Requirements_defects_linkage()

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



        Dim NumOfMaxLinks = Int(((reqNum) * defectsNum))
        If (Val(Form.ReqDefectLNK.Text)) > NumOfMaxLinks Then
            Throw New Exception("Given " & reqNum.ToString & " Requirements and " & defectsNum.ToString & " defects, Number of max linkages is " & NumOfMaxLinks.ToString & vbCrLf & "Refresh your combinatorics")
        End If


        For i = 1 To Val(Form.ReqDefectLNK.Text)
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

    Public Sub Handle_Req_test_coverage()

        Dim req As TDAPIOLELib.Req
        Dim Test As TDAPIOLELib.Test
        Dim rand As Integer
        Dim coverable As TDAPIOLELib.ICoverableReq

        Dim reqnum = ReqsList.Count
        Dim testnum = TestsList.Count

        Dim NumOfMaxLinks = Int(((reqnum) * testnum))
        If (Val(Form.TestReqLNK.Text)) > NumOfMaxLinks Then
            Throw New Exception("Given " & reqnum.ToString & " Requirements and " & testnum.ToString & " Test, Number of max Coverage is " & NumOfMaxLinks.ToString & vbCrLf & "Refresh your combinatorics")
        End If

        For i = 1 To Val(Form.TestReqLNK.Text)

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
End Module
