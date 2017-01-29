Imports System.ComponentModel

Public Interface IBindableControl
    Inherits INotifyPropertyChanged

    Property DataContext As Object

End Interface
