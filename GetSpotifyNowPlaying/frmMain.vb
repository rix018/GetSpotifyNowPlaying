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
                    UpdateText(Application.StartupPath & "\NowPlaying.txt", thisWindowTitle & "  ", True)
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
            Dim thisTask As New myTask

            If cmbType.Text = "Task (Check)" Or cmbType.Text = "Task (Uncheck)" Then
                If cmbType.Text = "Task (Check)" Then
                    thisTask = createMyTask(True, txtDesc.Text, "Task")
                Else
                    thisTask = createMyTask(False, txtDesc.Text, "Task")
                End If
            Else
                thisTask = createMyTask(False, txtDesc.Text, cmbType.Text)
            End If

            If txtChecklist.Text <> "" Then
                txtChecklist.Text = txtChecklist.Text.TrimEnd
                txtChecklist.Text &= vbCrLf
            End If

            If thisTask.TaskStatus Then
                txtChecklist.Text &= "Check" & "|" & thisTask.TaskDesc & "|" & thisTask.TaskType
            Else
                txtChecklist.Text &= "|" & thisTask.TaskDesc & "|" & thisTask.TaskType
            End If

            UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)
            myRefreshChecklistform()
        Else
            MsgBox("Choose type and description should not be blank")
        End If
    End Sub

    Private Sub chk_btnUpdate_Click(sender As Object, e As EventArgs) Handles chk_btnUpdate.Click
        UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)

        myRefreshChecklistform()

        MsgBox("Checklist Updated!", vbOKOnly)
    End Sub

    Private Sub chk_btnRefresh_Click(sender As Object, e As EventArgs) Handles chk_btnRefresh.Click
        txtChecklist.Text = GetText(Application.StartupPath & "\Checklist.txt")

        myRefreshChecklistform()
    End Sub

    Private Sub chk_btnClear_Click(sender As Object, e As EventArgs) Handles chk_btnClear.Click
        txtChecklist.Text = ""

        UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)

        myRefreshChecklistform()

        MsgBox("Clear Checklist!", vbOKOnly)
    End Sub

    Private Sub chk_btnWindow_Click(sender As Object, e As EventArgs) Handles chk_btnWindow.Click
        UpdateText(Application.StartupPath & "\Checklist.txt", txtChecklist.Text, False)
        txtChecklist.Text = GetText(Application.StartupPath & "\Checklist.txt")

        myReloadChecklist()

        frmChecklist.outChkTasks = myTasks
        frmChecklist.Show()
    End Sub

    Private Sub myReloadChecklist()
        Dim arrMyTask As New ArrayList
        myLoadTask(arrMyTask, myTasks)
    End Sub

    Private Sub myRefreshChecklistform()
        If frmChecklist.Visible Then

            Dim ogFormLoc As Point = frmChecklist.Location

            frmChecklist.Dispose()
            myReloadChecklist()

            frmChecklist.outChkTasks = myTasks
            frmChecklist.Show()
            frmChecklist.Location = ogFormLoc
        End If
    End Sub

End Class
