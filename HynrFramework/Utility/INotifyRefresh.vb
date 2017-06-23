Imports System.Collections.Specialized

Public Interface INotifyRefresh
    Inherits INotifyCollectionChanged
    Function OnRefresh()
End Interface