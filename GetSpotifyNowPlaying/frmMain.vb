Imports System.ComponentModel
Imports System.IO
Imports System.Threading

Public Class frmMain

    Private thisThread As Timer
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label.CheckForIllegalCrossThreadCalls = False

        If Not File.Exists(Application.StartupPath & "\NowPlaying.txt") Then
            File.Create(Application.StartupPath & "\NowPlaying.txt").Close()
            Thread.Sleep(2000)
        End If

        Label2.Text = ""

        thisStartThread()
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

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        thisThread.Dispose()
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

    Private Sub frmMain_MinimumSizeChanged(sender As Object, e As EventArgs) Handles Me.MinimumSizeChanged

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
End Class
