Public Class Math
    Private Shared _poundFactor As Double = 0.45359237
    Public Shared Function Pound2KG(ByVal pound As Double) As Double
        Return pound * _poundFactor
    End Function
    Public Shared Function KG2Pound(ByVal kg As Double) As Double
        Return kg / _poundFactor
    End Function

    Private Shared _inchFactor As Double = 2.54
    Public Shared Function Inch2Cm(ByVal inch As Double) As Double
        Return inch * _inchFactor
    End Function

    Public Shared Function PercentageVoltsTurn(ByVal N2 As Integer, ByVal uMax As Double, ByVal uNominal As Double) As Double
        Return (VoltsTurn(N2, uMax) / uNominal) * 100
    End Function

    Public Shared Function PercentageJ(ByVal jmax As Double, ByVal jNominal As Double) As Double
        Return (jmax / jNominal) * 100
    End Function

    Public Shared Function PercentageGreenLossLevel(ByVal value As Double, ByVal greenLevel As Double) As Double
        Return (value / greenLevel) * 100
    End Function

    Public Shared Function AmpsTurn(ByVal N1 As Integer, ByVal iEff As Double) As Double
        Return iEff * N1
    End Function

    Public Shared Function VoltsTurn(ByVal N2 As Integer, ByVal uMax As Double) As Double
        Return uMax * N2
    End Function

    Public Shared Function GetDgCoreGrade(ByVal greenLimit As Double, _
                                    ByVal yellowLimit As Double, _
                                    ByVal redLimit As Double, _
                                    ByVal gradeValue As Double) As DataElements.Result.GradeT
        If gradeValue <= greenLimit Then Return DataElements.Result.GradeT.Green
        If gradeValue <= yellowLimit Then Return DataElements.Result.GradeT.Yellow
        If gradeValue <= redLimit Then Return DataElements.Result.GradeT.Red

        Return DataElements.Result.GradeT.Rejected
    End Function
    Public Shared Function GetToroidGrade(ByVal passedLimit As Double, ByVal gradeValue As Double) As DataElements.Result.GradeT
        If gradeValue <= passedLimit Then Return DataElements.Result.GradeT.Passed

        Return DataElements.Result.GradeT.Rejected
    End Function

    Public Shared Function CalcDgCoreWeight(ByVal od As Double, _
                                            ByVal id As Double, _
                                            ByVal h As Double, _
                                            ByVal sf As Double, _
                                            ByVal density As Double) As Double
        Dim V As Double = (System.Math.PI * h / 4) * (od ^ 2 - id ^ 2)

        Return density * sf * V
    End Function
    Public Shared Function CalcToroidWeight(ByVal od As Double, _
                                            ByVal id As Double, _
                                            ByVal h As Double, _
                                            ByVal sf As Double, _
                                            ByVal density As Double) As Double
        Dim A As Double = System.Math.PI * (od ^ 2 - id ^ 2)

        Return A * h * density * sf
    End Function
End Class
