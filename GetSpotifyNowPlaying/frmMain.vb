Imports System.ComponentModel
Imports System.IO
Imports System.Threading

Public Class frmMain

    Public myTasks As New List(Of myTask)
    Private thisThread As Timer
    Private thisTimerThread As Timer
    Private iSeconds As Integer
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label.CheckForIllegalCrossThreadCalls = False

        IniThisFiles(Application.StartupPath & "\NowPlaying.txt")
        IniThisFiles(Application.StartupPath & "\Timer.txt")
        IniThisFiles(Application.StartupPath & "\PauseTXTMsg.txt")
        IniThisFiles(Application.StartupPath & "\Checklist.txt")

        Label2.Text = ""

        txtGetPauseMessage.Text = GetText(Application.StartupPath & "\PauseTXTMsg.txt")
        txtChecklist.Text = GetText(Application.StartupPath & "\Checklist.txt")

        thisStartThread()
    End Sub

    Private Sub IniThisFiles(ByVal sFullPath As String)
        Try
            If Not File.Exists(sFullPath) Then
                File.Create(sFullPath).Close()
                Thread.Sleep(2000)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub thisStartThread()
        Try
            thisThread = New Timer(AddressOf UpdateNowPlayingText, Nothing, 1000, 1000)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateNowPlayingText(ByVal state As Object)
        Dim processlist As Process() = Process.GetProcesses()

        For Each thisprocess In processlist
            If thisprocess.ProcessName = "Spotify" Then
                Dim thisWindowTitle As String = thisprocess.MainWindowTitle
                If thisWindowTitle <> "" AndAlso thisWindowTitle <> "Spotify" AndAlso thisWindowTitle <> "Spotify Premium" Then
                    Label2.Text = thisprocess.MainWindowTitle
                    UpdateText(Application.StartupPath & "\NowPlaying.txt", thisWindowTitle, True)
                End If
            End If
        Next
    End Sub

    Public Sub thisStartTimer()
        Try
            thisTimerThread = New Timer(AddressOf UpdateTimerLabel, Nothing, 1000, 1000)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateTimerLabel(ByVal state As Object)

        Dim sSeconds As String
        Dim tSpan As TimeSpan

        tSpan = TimeSpan.FromSeconds(iSeconds)

        sSeconds = tSpan.Hours.ToString.PadLeft(2, "0") & ":" & tSpan.Minutes.ToString.PadLeft(2, "0") & ":" & tSpan.Seconds.ToString.PadLeft(2, "0")

        Label3.Text = sSeconds

        UpdateText(Application.StartupPath & "\Timer.txt", sSeconds, True)

        iSeconds += 1
    End Sub

    Public Sub UpdateText(ByVal sTxtPath As String, ByVal sNowPlaying As String, ByVal bOneLine As Boolean)
        Try
            Application.DoEvents()

            File.WriteAllText(sTxtPath, "")

            Dim lines() As String = System.IO.File.ReadAllLines(sTxtPath)

            If lines.Count <> 0 Then
                If bOneLine Then
                    lines(0) = sNowPlaying
                    File.WriteAllLines(sTxtPath, lines)
                Else
                    For iLines = 0 To lines.Count - 1
                        lines(iLines) = sNowPlaying
                        File.WriteAllLines(sTxtPath, lines)
                    Next
                End If
            Else
                FileOpen(1, sTxtPath, OpenMode.Append)
                PrintLine(1, sNowPlaying)
                FileClose(1)
            End If

            System.Threading.Thread.Sleep(1000)
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetText(ByVal sTxtPath As String) As String
        Dim sReturn As String = ""

        Try
            sReturn = System.IO.File.ReadAllText(sTxtPath)

        Catch ex As Exception

        End Try

        Return sReturn
    End Function

    Public Function GetTextPerLine(ByVal sTxtPath As String) As ArrayList

        Dim sReturn As New ArrayList

        Try
            For Each line As String In File.ReadLines(sTxtPath)
                sReturn.Add(line)
            Next
        Catch ex As Exception

        End Try

        Return sReturn
    End Function

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        thisThread.Dispose()

        If btnTimer.Text = "Stop Timer" Then
            thisTimerThread.Dispose()
        End If
    End Sub

    Private Sub ContextMenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked
        If e.ClickedItem.ToString = "Open" Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.ShowInTaskbar = True

        ElseIf e.ClickedItem.ToString = "Quit" Then
            Try
                thisThread.Dispose()
                NotifyIcon1.Visible = False
                Me.Close()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub frmMain_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            Me.ShowInTaskbar = False
        Else
            NotifyIcon1.Visible = False
            Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub btnTimer_Click(sender As Object, e As EventArgs) Handles btnTimer.Click

        If btnTimer.Text = "Timer Start" Then
            iSeconds = 0
            thisStartTimer()
            btnTimer.Text = "Stop Timer"
        Else
            btnTimer.Text = "Timer Start"

            thisTimerThread.Dispose()

            iSeconds = 0

            Label3.Text = "00:00:00"

            UpdateText(Application.StartupPath & "\Timer.txt", "00:00:00", True)

        End If

    End Sub

    Private Sub btnPauseMsgUpdate_Click(sender As Object, e As EventArgs) Handles btnPauseMsgUpdate.Click
        UpdateText(Application.StartupPath & "\PauseTXTMsg.txt", txtGetPauseMessage.Text, False)

        MsgBox("Message Updated!", vbOKOnly)
    End Sub

    Private Sub btnPauseMsgRefresh_Click(sender As Object, e As EventArgs) Handles btnPauseMsgRefresh.Click
        txtGetPauseMessage.Text = GetText(Application.StartupPath & "\PauseTXTMsg.txt")
    End Sub

    Private Sub btnEmpty_Click(sender As Object, e As EventArgs) Handles btnEmpty.Click
        txtGetPauseMessage.Text = ""

        UpdateText(Application.StartupPath & "\PauseTXTMsg.txt", txtGetPauseMessage.Text, False)

        MsgBox("Clear Message!", vbOKOnly)
    End Sub

    Private Sub btnExpand_Click(sender As Object, e As EventArgs) Handles btnExpand.Click
        If Me.btnExpand.Text = ">>" Then
            Me.btnExpand.Text = "<<"
            Me.Width = 817
        Else
            Me.btnExpand.Text = ">>"
            Me.Width = 426
        End If
    End Sub

    Private Sub btnappend_Click(sender As Object, e As EventArgs) Handles btnappend.Click
        If cmbType.Text <> "" Or txtDesc.Text <> "" Then
            If txtChecklist.Text <> "" Then
                txtChecklist.Text = txtChecklist.Text.TrimEnd
                txtChecklist.Text &= vbCrLf
            End If

            If cmbType.Text = "Task (Check)" Then
                txtChecklist.Text &= "Check"
            End If

            txtChecklist.Text &= "|" & txtDesc.Text

            If cmbType.Text = "Header" Then
                txtChecklist.Text &= "|Header"
            Else
                txtChecklist.Text &= "|Task"
            End If
        Else
            MsgBox("Choose type and description should not be blank")
        End If
    End Sub

    Private Sub chk_btnUpdate_Click(sender As Object, e As EventArgs) Handles chk_btnUpdate.Click
        UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)

        MsgBox("Checklist Updated!", vbOKOnly)
    End Sub

    Private Sub chk_btnRefresh_Click(sender As Object, e As EventArgs) Handles chk_btnRefresh.Click
        txtChecklist.Text = GetText(Application.StartupPath & "\Checklist.txt")
    End Sub

    Private Sub chk_btnClear_Click(sender As Object, e As EventArgs) Handles chk_btnClear.Click
        txtChecklist.Text = ""

        UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)

        MsgBox("Clear Checklist!", vbOKOnly)
    End Sub

    Private Sub chk_btnWindow_Click(sender As Object, e As EventArgs) Handles chk_btnWindow.Click
        myTasks.Clear()

        UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)

        txtChecklist.Text = GetText(Application.StartupPath & "\Checklist.txt")

        Dim arrMyTask As New ArrayList
        arrMyTask = GetTextPerLine(Application.StartupPath & "\Checklist.txt")

        'To Refactor
        For Each sTask As String In arrMyTask
            If sTask <> "" Then
                Dim sTaskSplit As String()
                sTaskSplit = sTask.Split("|")

                Dim clMyTask As New myTask

                clMyTask.TaskStatus = False
                If sTaskSplit(2) = "Task" Then
                    If sTaskSplit(0) = "Check" Then
                        clMyTask.TaskStatus = True
                    End If
                Else
                    clMyTask.TaskStatus = False
                End If

                clMyTask.TaskDesc = sTaskSplit(1)

                clMyTask.TaskType = sTaskSplit(2)

                myTasks.Add(clMyTask)
            End If
        Next

        frmChecklist.outChkTasks = myTasks

        frmChecklist.Show()


    End Sub

End Class
