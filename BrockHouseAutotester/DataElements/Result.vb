Imports System.ComponentModel

Namespace DataElements
    Public Class Result

        <DisplayName("Serial No."), BrowsableAttribute(True)>
        Public Property Serial As String

        <DisplayName("Sub Serial No."), BrowsableAttribute(True)>
        Public ReadOnly Property Test As String
            Get
                If DuplicateTest <> "" AndAlso DuplicateTest IsNot Nothing AndAlso Retest <> 0 Then
                    Return "-" + DuplicateTest + "-" + Retest.ToString
                End If
                If Retest <> 0 Then Return "-" + Retest.ToString
                If DuplicateTest <> "" AndAlso DuplicateTest IsNot Nothing Then Return "-" + DuplicateTest
                Return ""
            End Get
        End Property

        <DisplayName("Date"), BrowsableAttribute(True)>
        Public Property [Date] As System.DateTime

        <DisplayName("Operator"), BrowsableAttribute(True)>
        Public Property User As String

        <DisplayName("Mpg device"), BrowsableAttribute(True)>
        Public Property MpgDevice As String

        <DisplayName("Weight [lbs]"), BrowsableAttribute(True)>
        Public Property Weight As Single

        'Toroid
        <DisplayName("Volts")>
        Public Property Volts As Single

        <DisplayName("Amps")>
        Public Property Amps As Single

        <DisplayName("Number of Turns"), BrowsableAttribute(True)>
        Public Property NumberOfTurns As Integer

        'DgCore
        <DisplayName("Watts"), BrowsableAttribute(True)>
        Public Property Watts As Single

        <DisplayName("AT-rms")>
        Public Property AmpTurns As Single

        <DisplayName("Grade"), BrowsableAttribute(True)>
        Property Grade As GradeT

        <DisplayName("% Typ"), BrowsableAttribute(True)>
        Property PercentageGreenLossLevel As Double



        <BrowsableAttribute(False)>
        Public Property PercentageActualJ As Single
        <BrowsableAttribute(False)>
        Public Property State() As StateT
        <BrowsableAttribute(False)>
        Public Property Retest As Integer            'Init 0 - Retest = 1/2/3 etc
        <BrowsableAttribute(False)>
        Public Property DuplicateTest As String     'Init 0 - Retest = Duplicate test = A/B/C etc
        <BrowsableAttribute(False)>
        Public ReadOnly Property SortedSerial As String
            Get
                Select Case Serial.Length
                    Case 0 : Return Serial
                    Case 1 : Return Serial.Insert(0, "00000")
                    Case 2 : Return Serial.Insert(0, "0000")
                    Case 3 : Return Serial.Insert(0, "000")
                    Case 4 : Return Serial.Insert(0, "00")
                    Case 5 : Return Serial.Insert(0, "0")
                    Case Else : Return Serial
                End Select
            End Get
        End Property
        Public Enum StateT
            Accept
            Abandon
            Overwrite
            Reprocessed
            Remade
        End Enum
        Public Enum GradeT
            Passed
            Green
            Yellow
            Red
            Rejected
            IOR 'Induction Reached is Out of Range
        End Enum
    End Class
End Namespace
