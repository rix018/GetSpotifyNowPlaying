Public Class myTask
    Public zTaskStatus As Boolean
    Public zTaskDesc As String
    Public zTaskType As String

    Public Sub New()
        MyBase.New
    End Sub

    Public Property TaskStatus As Boolean
        Get
            Return zTaskStatus
        End Get
        Set(value As Boolean)
            zTaskStatus = value
        End Set
    End Property

    Public Property TaskDesc As String
        Get
            Return zTaskDesc
        End Get
        Set(value As String)
            zTaskDesc = value
        End Set
    End Property

    Public Property TaskType As String
        Get
            Return zTaskType
        End Get
        Set(value As String)
            zTaskType = value
        End Set

    End Property

End Class
