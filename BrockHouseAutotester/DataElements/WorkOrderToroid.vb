Imports System.ComponentModel

Namespace DataElements
    Public Class WorkOrderToroid
        Inherits WorkOrder

        <DisplayName("Max amps"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property MaxAmps As Double

        <DisplayName("Number of turns"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property NumberOfTurns As Integer

        <DisplayName("Operator Max amps"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property OperatorMaxAmps As Double

        <DisplayName("Operator Number of turns"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property OperatorNumberOfTurns As Integer
    End Class
End Namespace

