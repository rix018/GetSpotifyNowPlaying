Imports System.ComponentModel
Imports System.IO
Imports System.Threading

Public Class frmMain

    Private thisThread As Timer
    Private thisTimerThread As Timer
    Private iSeconds As Integer
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label.CheckForIllegalCrossThreadCalls = False

        IniThisFiles(Application.StartupPath & "\NowPlaying.txt")
        IniThisFiles(Application.StartupPath & "\Timer.txt")
        IniThisFiles(Application.StartupPath & "\PauseTXTMsg.txt")

        Label2.Text = ""

        txtGetPauseMessage.Text = GetText(Application.StartupPath & "\PauseTXTMsg.txt")

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
                    UpdateText(Application.StartupPath & "\NowPlaying.txt", thisWindowTitle)
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

        UpdateText(Application.StartupPath & "\Timer.txt", sSeconds)

        iSeconds += 1
    End Sub

    Private Sub UpdateText(ByVal sTxtPath As String, ByVal sNowPlaying As String)
        Try
            Dim lines() As String = System.IO.File.ReadAllLines(sTxtPath)

            If lines.Count <> 0 Then
                lines(0) = sNowPlaying
                File.WriteAllLines(sTxtPath, lines)
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

            UpdateText(Application.StartupPath & "\Timer.txt", "00:00:00")

        End If

    End Sub

    Private Sub btnPauseMsgUpdate_Click(sender As Object, e As EventArgs) Handles btnPauseMsgUpdate.Click
        UpdateText(Application.StartupPath & "\PauseTXTMsg.txt", txtGetPauseMessage.Text)

        MsgBox("Message Updated!", vbOKOnly)
    End Sub

    Private Sub btnPauseMsgRefresh_Click(sender As Object, e As EventArgs) Handles btnPauseMsgRefresh.Click
        txtGetPauseMessage.Text = GetText(Application.StartupPath & "\PauseTXTMsg.txt")
    End Sub
End Class
