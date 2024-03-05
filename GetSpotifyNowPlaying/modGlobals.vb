Imports System.IO

Module modGlobals

    Public Sub myLoadTask(ByVal thisArr As ArrayList, ByRef thisUpdatedArr As List(Of myTask))

        thisArr = GetTextPerLine(Application.StartupPath & "\Checklist.txt")

        For Each sTask As String In thisArr
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

                thisUpdatedArr.Add(clMyTask)
            End If
        Next
    End Sub

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

    Public Function GetText(ByVal sTxtPath As String) As String
        Dim sReturn As String = ""

        Try
            sReturn = System.IO.File.ReadAllText(sTxtPath)

        Catch ex As Exception

        End Try

        Return sReturn
    End Function

End Module
