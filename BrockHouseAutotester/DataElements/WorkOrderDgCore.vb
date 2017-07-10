Imports System.ComponentModel

Namespace DataElements
    Public Class WorkOrderDgCore
        Inherits WorkOrder

        <DisplayName("Max green"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property LossLimit_MaxGreen As Double
        <DisplayName("Max yellow"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property LossLimit_MaxYellow As Double
        <DisplayName("Max red"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property LossLimit_MaxRed As Double

    End Class
End Namespace
