Namespace DataElements
    Public Structure Sample
        Public SerialNo As String

        'RectangularCore
        Public StackingFactor As Double

        'Toroid

        'Both
        Public InnerDiameter As Double
        Public OuterDiameter As Double

        Public weight As Double
        Public density As Double
        Public MaterialWidth As Double

        Public SampleType As SampleTypeT

        Public Enum SampleTypeT
            DgCore
            Toroid
        End Enum
    End Structure
End Namespace
