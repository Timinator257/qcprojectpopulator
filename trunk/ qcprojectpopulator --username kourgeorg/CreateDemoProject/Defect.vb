Module Defect
    Public DefectsList As TDAPIOLELib.List

    Public Sub Handle_Defects()
        Dim bfact As TDAPIOLELib.BugFactory
        bfact = tdc.BugFactory
        Dim mybug As TDAPIOLELib.Bug
        Dim high, meduim, low, veryHigh, Critical As Integer
        Critical = (Val(Form.DefectsNum.Text) * Val(Form.CriticalTextBox.Text)) / 100
        veryHigh = (Val(Form.DefectsNum.Text) * Val(Form.VeryHighTextBox.Text)) / 100 + Critical
        high = (Val(Form.DefectsNum.Text) * Val(Form.HighTextBox.Text)) / 100 + veryHigh
        meduim = (Val(Form.DefectsNum.Text) * Val(Form.MeduimTextBox.Text)) / 100 + high
        low = (Val(Form.DefectsNum.Text) * Val(Form.LowTextBox.Text)) / 100 + meduim

        AddEventToLog(1, "start Creating Defects")
        'add defects and determine severity and priority
        For i = 1 To Val(Form.DefectsNum.Text)
            mybug = bfact.AddItem(DBNull.Value)
            mybug.Summary = "Defect" & i & " " & LoginTime
            Dim mydate = Now.Date.ToString.Substring(0, 10)
            mybug.Field("BG_DETECTION_DATE") = mydate
            mybug.Status = "New"
            mybug.Field("BG_DESCRIPTION") = Form.DefectDescription.Text
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
            mybug.DetectedBy = Form.Username.Text
            mybug.Post()
            DefectsList.Add(mybug)
            AddEventToLog(1, "Defect " & mybug.ID & " was added")
            'Handle attachement in defect
            If Form.AttachmentCheckBox.Enabled Then
                If i <= Val(Form.DefectAttachment.Text) Then
                    Dim attachF As TDAPIOLELib.AttachmentFactory = mybug.Attachments
                    Dim attachment As TDAPIOLELib.Attachment = attachF.AddItem(DBNull.Value)
                    attachment.FileName = Form.AttachementTextBox.Text
                    attachment.Type = TDAPIOLELib.TDAPI_ATTACH_TYPE.TDATT_FILE
                    attachment.Post()
                    AddEventToLog(1, "Attachement was added to defect " & mybug.ID)
                End If
            End If
        Next i

        'Handle statuses
        Dim open, fixed, closed, rejected, reopen As Integer
        open = (Val(Form.DefectsNum.Text) * Val(Form.OpenTextBox.Text)) / 100
        fixed = (Val(Form.DefectsNum.Text) * Val(Form.FixTextBox.Text)) / 100 + open
        closed = (Val(Form.DefectsNum.Text) * Val(Form.CloseTextBox.Text)) / 100 + fixed
        rejected = (Val(Form.DefectsNum.Text) * Val(Form.RejectTextBox.Text)) / 100 + closed
        reopen = (Val(Form.DefectsNum.Text) * Val(Form.ReopenTextBox.Text)) / 100 + rejected


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

    Public Sub Handle_Defects_Relations()

        Dim bfact As TDAPIOLELib.BugFactory
        bfact = tdc.BugFactory


        Dim ListdefectsNum As Long
        ListdefectsNum = DefectsList.Count

        Dim NumOfMaxLinks = Int(((ListdefectsNum - 1) * ListdefectsNum) / 2)
        If (Val(Form.DefectDefectLNK.Text)) > NumOfMaxLinks Then
            Throw New Exception("Given " & ListdefectsNum.ToString & " Defects, Number of Max Links between defects is " & NumOfMaxLinks.ToString & vbCrLf & "Refresh your combinatorics")
        End If

        Dim linkF As TDAPIOLELib.LinkFactory
        Dim link As TDAPIOLELib.Link
        Dim ilink1 As TDAPIOLELib.ILinkable


        Dim numDefectsToLink As Long
        numDefectsToLink = Val(Form.DefectDefectLNK.Text)
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
End Module
