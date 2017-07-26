Imports System.ComponentModel

Namespace Settings
    <Serializable()>
    Public Class Settings
        <Category("MPG"), DisplayName("device ID"), BrowsableAttribute(True)>
        Public Property MpgDeviceId As String = "MPG"

        <Category("Printer"), DisplayName("Print Layout"), BrowsableAttribute(True)>
        Public Property PrinterLayoutOption As PrinterLayoutOptionT

        <Category("Harness"), DisplayName("Default Harness N1"), Browsable(True)>
        Public Property HarnessDefaultN1 As Integer
        <Category("Harness"), DisplayName("Default Harness N2"), Browsable(True)>
        Public Property HarnessDefaultN2 As Integer

        <Category("Measurement"), DisplayName("Default density [g/dm³]"), Browsable(True)>
        Public Property DensityDefault As Double = 7650
        <Category("Measurement"), DisplayName("Default frequency [Hz]"), Browsable(True)>
        Public Property FrequencyDefault As Double = 60
        <Category("Measurement"), DisplayName("Weight range +- [%]"), Browsable(True)>
        Public Property WeightRange As Double = 4
        <Category("Measurement"), DisplayName("Toroid default stacking factor"), Browsable(True)>
        Public Property ToroidDefaultStackingFactor As Double = 1
        <Category("Measurement"), DisplayName("Low loss warning percentage of green limit"), Browsable(True)>
        Public Property LowLossWarningPercentageOfGreenLimit As Double = 80

        <Category("Grade color"), DisplayName("Passed"), Browsable(True), Xml.Serialization.XmlIgnore()>
        Public Property ColorPassed As Color
        <Category("Grade color"), DisplayName("Green"), Browsable(True), Xml.Serialization.XmlIgnore()>
        Public Property ColorGreen As Color
        <Category("Grade color"), DisplayName("Yellow"), Browsable(True), Xml.Serialization.XmlIgnore()>
        Public Property ColorYellow As Color
        <Category("Grade color"), DisplayName("Red"), Browsable(True), Xml.Serialization.XmlIgnore()>
        Public Property ColorRed As Color
        <Category("Grade color"), DisplayName("Blue"), Browsable(True), Xml.Serialization.XmlIgnore()>
        Public Property ColorRejected As Color

        <Category("SQL credentials"), DisplayName("Server"), BrowsableAttribute(True)>
        Public Property DataSource As String = "121COGENTS9K"
        <Category("SQL credentials"), DisplayName("UserID"), BrowsableAttribute(True)>
        Public Property UserID As String = "SYSADM"
        <Category("SQL credentials"), DisplayName("Password"), PasswordPropertyText(True), BrowsableAttribute(True)>
        Public Property Password As String = "COPOW05"

        <Browsable(False)>
       Public Property ColorPssedSerializable As Integer
            Set(value As Integer)
                ColorPassed = Drawing.Color.FromArgb(value)
            End Set
            Get
                Return ColorPassed.ToArgb
            End Get
        End Property
        <Browsable(False)>
        Public Property ColorGreenSerializable As Integer
            Set(value As Integer)
                ColorGreen = Drawing.Color.FromArgb(value)
            End Set
            Get
                Return ColorGreen.ToArgb
            End Get
        End Property
        <Browsable(False)>
        Public Property ColorYellowSerializable As Integer
            Set(value As Integer)
                ColorYellow = Drawing.Color.FromArgb(value)
            End Set
            Get
                Return ColorYellow.ToArgb()
            End Get
        End Property
        <Browsable(False)>
        Public Property ColorRedSerializable As Integer
            Set(value As Integer)
                ColorRed = Drawing.Color.FromArgb(value)
            End Set
            Get
                Return ColorRed.ToArgb
            End Get
        End Property
        <Browsable(False)>
        Public Property ColorRejectedSerializable As Integer
            Set(value As Integer)
                ColorRejected = Drawing.Color.FromArgb(value)
            End Set
            Get
                Return ColorRejected.ToArgb
            End Get
        End Property
    End Class
    Public Enum PrinterLayoutOptionT
        Full
        [Partial]
        Off
    End Enum
End Namespace
