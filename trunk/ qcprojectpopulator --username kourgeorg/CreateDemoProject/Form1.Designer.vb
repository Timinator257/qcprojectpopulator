<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ServerURL = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.DomainComboBox = New System.Windows.Forms.ComboBox
        Me.ProjectsComboBox = New System.Windows.Forms.ComboBox
        Me.LoginButton = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Password = New System.Windows.Forms.TextBox
        Me.Username = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Result = New System.Windows.Forms.TextBox
        Me.Populate = New System.Windows.Forms.Button
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.Label7 = New System.Windows.Forms.Label
        Me.DefectsNum = New System.Windows.Forms.TextBox
        Me.DefectsCheckBox = New System.Windows.Forms.CheckBox
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.TotalTextBox = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.CriticalTextBox = New System.Windows.Forms.TextBox
        Me.VeryHighTextBox = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.LowTextBox = New System.Windows.Forms.TextBox
        Me.MeduimTextBox = New System.Windows.Forms.TextBox
        Me.HighTextBox = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label31 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TestCoverageTextBox = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.ReqLevels = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.ReqCheckBox = New System.Windows.Forms.CheckBox
        Me.ReqsNum = New System.Windows.Forms.TextBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Label14 = New System.Windows.Forms.Label
        Me.TestsInLevel = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TestDirLevels = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.DirsInLevel = New System.Windows.Forms.TextBox
        Me.TestCheckBox = New System.Windows.Forms.CheckBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.LinksBetweenDefects = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.LinkCheckBox = New System.Windows.Forms.CheckBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.ReqDefectlnk = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TestDefectLnk = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.ProgressBar = New System.Windows.Forms.ProgressBar
        Me.Label24 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Password"
        '
        'ServerURL
        '
        Me.ServerURL.Location = New System.Drawing.Point(113, 14)
        Me.ServerURL.Name = "ServerURL"
        Me.ServerURL.Size = New System.Drawing.Size(206, 20)
        Me.ServerURL.TabIndex = 4
        Me.ServerURL.Text = "http://localhost:8080/qcbin"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.DomainComboBox)
        Me.Panel1.Controls.Add(Me.ProjectsComboBox)
        Me.Panel1.Controls.Add(Me.LoginButton)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Password)
        Me.Panel1.Controls.Add(Me.Username)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.ServerURL)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(12, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(382, 169)
        Me.Panel1.TabIndex = 5
        '
        'DomainComboBox
        '
        Me.DomainComboBox.FormattingEnabled = True
        Me.DomainComboBox.Location = New System.Drawing.Point(113, 100)
        Me.DomainComboBox.Name = "DomainComboBox"
        Me.DomainComboBox.Size = New System.Drawing.Size(121, 21)
        Me.DomainComboBox.TabIndex = 15
        '
        'ProjectsComboBox
        '
        Me.ProjectsComboBox.FormattingEnabled = True
        Me.ProjectsComboBox.Location = New System.Drawing.Point(113, 129)
        Me.ProjectsComboBox.Name = "ProjectsComboBox"
        Me.ProjectsComboBox.Size = New System.Drawing.Size(121, 21)
        Me.ProjectsComboBox.TabIndex = 14
        '
        'LoginButton
        '
        Me.LoginButton.Location = New System.Drawing.Point(267, 70)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(85, 27)
        Me.LoginButton.TabIndex = 13
        Me.LoginButton.Text = "Authenticate"
        Me.LoginButton.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Domain Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Project Name"
        '
        'Password
        '
        Me.Password.Location = New System.Drawing.Point(113, 74)
        Me.Password.Name = "Password"
        Me.Password.Size = New System.Drawing.Size(100, 20)
        Me.Password.TabIndex = 6
        '
        'Username
        '
        Me.Username.Location = New System.Drawing.Point(113, 44)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(100, 20)
        Me.Username.TabIndex = 5
        Me.Username.Text = "sa"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Username"
        '
        'Result
        '
        Me.Result.Enabled = False
        Me.Result.Location = New System.Drawing.Point(54, 13)
        Me.Result.Multiline = True
        Me.Result.Name = "Result"
        Me.Result.Size = New System.Drawing.Size(304, 61)
        Me.Result.TabIndex = 8
        '
        'Populate
        '
        Me.Populate.Location = New System.Drawing.Point(327, 483)
        Me.Populate.Name = "Populate"
        Me.Populate.Size = New System.Drawing.Size(67, 25)
        Me.Populate.TabIndex = 7
        Me.Populate.Text = "Populate"
        Me.Populate.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Controls.Add(Me.TabPage3)
        Me.TabControl.Controls.Add(Me.TabPage4)
        Me.TabControl.Controls.Add(Me.TabPage5)
        Me.TabControl.Location = New System.Drawing.Point(12, 215)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(382, 164)
        Me.TabControl.TabIndex = 19
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.TabControl1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(374, 138)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Defects"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(0, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(378, 137)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage6.Controls.Add(Me.Label7)
        Me.TabPage6.Controls.Add(Me.DefectsNum)
        Me.TabPage6.Controls.Add(Me.DefectsCheckBox)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(370, 111)
        Me.TabPage6.TabIndex = 0
        Me.TabPage6.Text = "General"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "Total Quantity"
        '
        'DefectsNum
        '
        Me.DefectsNum.Location = New System.Drawing.Point(91, 24)
        Me.DefectsNum.Name = "DefectsNum"
        Me.DefectsNum.Size = New System.Drawing.Size(49, 20)
        Me.DefectsNum.TabIndex = 27
        Me.DefectsNum.Text = "100"
        '
        'DefectsCheckBox
        '
        Me.DefectsCheckBox.AutoSize = True
        Me.DefectsCheckBox.Location = New System.Drawing.Point(6, 6)
        Me.DefectsCheckBox.Name = "DefectsCheckBox"
        Me.DefectsCheckBox.Size = New System.Drawing.Size(61, 17)
        Me.DefectsCheckBox.TabIndex = 28
        Me.DefectsCheckBox.Text = "Include"
        Me.DefectsCheckBox.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage7.Controls.Add(Me.TotalTextBox)
        Me.TabPage7.Controls.Add(Me.Label29)
        Me.TabPage7.Controls.Add(Me.Label21)
        Me.TabPage7.Controls.Add(Me.Label28)
        Me.TabPage7.Controls.Add(Me.Label27)
        Me.TabPage7.Controls.Add(Me.Label26)
        Me.TabPage7.Controls.Add(Me.Label25)
        Me.TabPage7.Controls.Add(Me.CriticalTextBox)
        Me.TabPage7.Controls.Add(Me.VeryHighTextBox)
        Me.TabPage7.Controls.Add(Me.Label23)
        Me.TabPage7.Controls.Add(Me.Label22)
        Me.TabPage7.Controls.Add(Me.Label20)
        Me.TabPage7.Controls.Add(Me.Label19)
        Me.TabPage7.Controls.Add(Me.Label18)
        Me.TabPage7.Controls.Add(Me.LowTextBox)
        Me.TabPage7.Controls.Add(Me.MeduimTextBox)
        Me.TabPage7.Controls.Add(Me.HighTextBox)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(370, 111)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "Severity"
        '
        'TotalTextBox
        '
        Me.TotalTextBox.Enabled = False
        Me.TotalTextBox.Location = New System.Drawing.Point(307, 85)
        Me.TotalTextBox.Name = "TotalTextBox"
        Me.TotalTextBox.Size = New System.Drawing.Size(49, 20)
        Me.TotalTextBox.TabIndex = 25
        Me.TotalTextBox.Text = "0"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(106, 54)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(20, 16)
        Me.Label29.TabIndex = 34
        Me.Label29.Text = "%"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(253, 89)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(42, 13)
        Me.Label21.TabIndex = 22
        Me.Label21.Text = "Total %"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(106, 34)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(20, 16)
        Me.Label28.TabIndex = 33
        Me.Label28.Text = "%"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(106, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(20, 16)
        Me.Label27.TabIndex = 32
        Me.Label27.Text = "%"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(233, 37)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(20, 16)
        Me.Label26.TabIndex = 31
        Me.Label26.Text = "%"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(233, 15)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(20, 16)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "%"
        '
        'CriticalTextBox
        '
        Me.CriticalTextBox.Location = New System.Drawing.Point(184, 11)
        Me.CriticalTextBox.Name = "CriticalTextBox"
        Me.CriticalTextBox.Size = New System.Drawing.Size(49, 20)
        Me.CriticalTextBox.TabIndex = 29
        Me.CriticalTextBox.Text = "0"
        '
        'VeryHighTextBox
        '
        Me.VeryHighTextBox.Location = New System.Drawing.Point(184, 32)
        Me.VeryHighTextBox.Name = "VeryHighTextBox"
        Me.VeryHighTextBox.Size = New System.Drawing.Size(49, 20)
        Me.VeryHighTextBox.TabIndex = 28
        Me.VeryHighTextBox.Text = "0"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(130, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(38, 13)
        Me.Label23.TabIndex = 27
        Me.Label23.Text = "Critical"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(130, 35)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 13)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "Very High"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(4, 54)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(27, 13)
        Me.Label20.TabIndex = 24
        Me.Label20.Text = "Low"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(2, 34)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(44, 13)
        Me.Label19.TabIndex = 23
        Me.Label19.Text = "Medium"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 14)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(29, 13)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "High"
        '
        'LowTextBox
        '
        Me.LowTextBox.Location = New System.Drawing.Point(57, 51)
        Me.LowTextBox.Name = "LowTextBox"
        Me.LowTextBox.Size = New System.Drawing.Size(49, 20)
        Me.LowTextBox.TabIndex = 20
        Me.LowTextBox.Text = "0"
        '
        'MeduimTextBox
        '
        Me.MeduimTextBox.Location = New System.Drawing.Point(57, 31)
        Me.MeduimTextBox.Name = "MeduimTextBox"
        Me.MeduimTextBox.Size = New System.Drawing.Size(49, 20)
        Me.MeduimTextBox.TabIndex = 19
        Me.MeduimTextBox.Text = "0"
        '
        'HighTextBox
        '
        Me.HighTextBox.Location = New System.Drawing.Point(57, 11)
        Me.HighTextBox.Name = "HighTextBox"
        Me.HighTextBox.Size = New System.Drawing.Size(49, 20)
        Me.HighTextBox.TabIndex = 18
        Me.HighTextBox.Text = "0"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.Label31)
        Me.TabPage2.Controls.Add(Me.TextBox2)
        Me.TabPage2.Controls.Add(Me.Label30)
        Me.TabPage2.Controls.Add(Me.TestCoverageTextBox)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.ReqLevels)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.ReqCheckBox)
        Me.TabPage2.Controls.Add(Me.ReqsNum)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(374, 138)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Requirements"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(3, 97)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(78, 13)
        Me.Label31.TabIndex = 22
        Me.Label31.Text = "Req Tracability"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(112, 96)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(49, 20)
        Me.TextBox2.TabIndex = 21
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(3, 74)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(77, 13)
        Me.Label30.TabIndex = 20
        Me.Label30.Text = "Test Coverage"
        '
        'TestCoverageTextBox
        '
        Me.TestCoverageTextBox.Location = New System.Drawing.Point(112, 72)
        Me.TestCoverageTextBox.Name = "TestCoverageTextBox"
        Me.TestCoverageTextBox.Size = New System.Drawing.Size(49, 20)
        Me.TestCoverageTextBox.TabIndex = 19
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 28)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Reqs Levels"
        '
        'ReqLevels
        '
        Me.ReqLevels.Location = New System.Drawing.Point(112, 24)
        Me.ReqLevels.Name = "ReqLevels"
        Me.ReqLevels.Size = New System.Drawing.Size(49, 20)
        Me.ReqLevels.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Num of Reqs in level"
        '
        'ReqCheckBox
        '
        Me.ReqCheckBox.AutoSize = True
        Me.ReqCheckBox.Location = New System.Drawing.Point(6, 6)
        Me.ReqCheckBox.Name = "ReqCheckBox"
        Me.ReqCheckBox.Size = New System.Drawing.Size(61, 17)
        Me.ReqCheckBox.TabIndex = 15
        Me.ReqCheckBox.Text = "Include"
        Me.ReqCheckBox.UseVisualStyleBackColor = True
        '
        'ReqsNum
        '
        Me.ReqsNum.Location = New System.Drawing.Point(112, 48)
        Me.ReqsNum.Name = "ReqsNum"
        Me.ReqsNum.Size = New System.Drawing.Size(49, 20)
        Me.ReqsNum.TabIndex = 13
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Controls.Add(Me.TestsInLevel)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.TestDirLevels)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.DirsInLevel)
        Me.TabPage3.Controls.Add(Me.TestCheckBox)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(374, 138)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TestPlan"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(2, 73)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(69, 13)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Tests in level"
        '
        'TestsInLevel
        '
        Me.TestsInLevel.Location = New System.Drawing.Point(79, 69)
        Me.TestsInLevel.Name = "TestsInLevel"
        Me.TestsInLevel.Size = New System.Drawing.Size(49, 20)
        Me.TestsInLevel.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(2, 28)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "levels of Dirs"
        '
        'TestDirLevels
        '
        Me.TestDirLevels.Location = New System.Drawing.Point(79, 22)
        Me.TestDirLevels.Name = "TestDirLevels"
        Me.TestDirLevels.Size = New System.Drawing.Size(49, 20)
        Me.TestDirLevels.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(2, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Dirs in level"
        '
        'DirsInLevel
        '
        Me.DirsInLevel.Location = New System.Drawing.Point(79, 45)
        Me.DirsInLevel.Name = "DirsInLevel"
        Me.DirsInLevel.Size = New System.Drawing.Size(49, 20)
        Me.DirsInLevel.TabIndex = 20
        '
        'TestCheckBox
        '
        Me.TestCheckBox.AutoSize = True
        Me.TestCheckBox.Location = New System.Drawing.Point(6, 6)
        Me.TestCheckBox.Name = "TestCheckBox"
        Me.TestCheckBox.Size = New System.Drawing.Size(61, 17)
        Me.TestCheckBox.TabIndex = 21
        Me.TestCheckBox.Text = "Include"
        Me.TestCheckBox.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(374, 138)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "TestLab"
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage5.Controls.Add(Me.LinksBetweenDefects)
        Me.TabPage5.Controls.Add(Me.Label32)
        Me.TabPage5.Controls.Add(Me.LinkCheckBox)
        Me.TabPage5.Controls.Add(Me.Label15)
        Me.TabPage5.Controls.Add(Me.ReqDefectlnk)
        Me.TabPage5.Controls.Add(Me.Label16)
        Me.TabPage5.Controls.Add(Me.TestDefectLnk)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(374, 138)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Linkage"
        '
        'LinksBetweenDefects
        '
        Me.LinksBetweenDefects.Location = New System.Drawing.Point(117, 70)
        Me.LinksBetweenDefects.Name = "LinksBetweenDefects"
        Me.LinksBetweenDefects.Size = New System.Drawing.Size(49, 20)
        Me.LinksBetweenDefects.TabIndex = 35
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(3, 73)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(111, 13)
        Me.Label32.TabIndex = 34
        Me.Label32.Text = "Defects <==> Defects"
        '
        'LinkCheckBox
        '
        Me.LinkCheckBox.AutoSize = True
        Me.LinkCheckBox.Location = New System.Drawing.Point(6, 6)
        Me.LinkCheckBox.Name = "LinkCheckBox"
        Me.LinkCheckBox.Size = New System.Drawing.Size(61, 17)
        Me.LinkCheckBox.TabIndex = 33
        Me.LinkCheckBox.Text = "Include"
        Me.LinkCheckBox.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(2, 28)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(99, 13)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "Reqs <==> Defects"
        '
        'ReqDefectlnk
        '
        Me.ReqDefectlnk.Location = New System.Drawing.Point(117, 25)
        Me.ReqDefectlnk.Name = "ReqDefectlnk"
        Me.ReqDefectlnk.Size = New System.Drawing.Size(49, 20)
        Me.ReqDefectlnk.TabIndex = 31
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(2, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 13)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "Tests <==> Defects"
        '
        'TestDefectLnk
        '
        Me.TestDefectLnk.Location = New System.Drawing.Point(117, 47)
        Me.TestDefectLnk.Name = "TestDefectLnk"
        Me.TestDefectLnk.Size = New System.Drawing.Size(49, 20)
        Me.TestDefectLnk.TabIndex = 29
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.Label8.Location = New System.Drawing.Point(30, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(210, 24)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Demo Project Creator"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Result"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Result)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Location = New System.Drawing.Point(12, 383)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(382, 83)
        Me.Panel3.TabIndex = 14
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(67, 485)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(249, 23)
        Me.ProgressBar.TabIndex = 14
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(14, 489)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(48, 13)
        Me.Label24.TabIndex = 20
        Me.Label24.Text = "Progress"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(251, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(120, 22)
        Me.PictureBox1.TabIndex = 21
        Me.PictureBox1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 516)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Populate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.Text = "Demo Project Creator"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ServerURL As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Result As System.Windows.Forms.TextBox
    Friend WithEvents Populate As System.Windows.Forms.Button
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents Username As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ReqsNum As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ReqCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DirsInLevel As System.Windows.Forms.TextBox
    Friend WithEvents TestCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TestDirLevels As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ReqLevels As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TestsInLevel As System.Windows.Forms.TextBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents LinkCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ReqDefectlnk As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TestDefectLnk As System.Windows.Forms.TextBox
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents ProjectsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DomainComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TestCoverageTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LinksBetweenDefects As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DefectsNum As System.Windows.Forms.TextBox
    Friend WithEvents DefectsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TotalTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents CriticalTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VeryHighTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents LowTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MeduimTextBox As System.Windows.Forms.TextBox
    Friend WithEvents HighTextBox As System.Windows.Forms.TextBox

End Class
