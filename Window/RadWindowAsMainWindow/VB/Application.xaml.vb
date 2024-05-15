Imports System.Configuration

Class Application
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        CType(New MainWindow(), MainWindow).Show()
        MyBase.OnStartup(e)
    End Sub

End Class
