Imports System.Diagnostics
Imports System.Reflection

Namespace Common
    ''' <summary>
    ''' 
    ''' </summary>
    Public Class ProgramConstants

        Public Shared LoginLevel As Int32 = 0
        Public Const ChannelHeader As String = "{Ch_"
        Public Const ChannelFooter As String = "_Ch}"
        'public const string ABClassHeader = "{TT_";
        'public const string ABClassFooter = "_TT}";
        Public Const AddressHeader As String = "{Adrs_"
        Public Const AddressFooter As String = "_Adrs}"

        Public Const FailedRead As String = "{***Read Failure***}"
        Public Shared ABDataReadErrors As Integer = 0
        Public Shared ABDataWriteErrors As Integer = 0


        Public Shared assemblyVersion As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
        Public Shared fileVersion As String = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion
        Public Shared productVersion As String = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion

        Public Shared ProgramVersion As String = "Assembly Version : " + assemblyVersion + "   File Version : " + fileVersion + "  Product Version : " + productVersion



        Public Shared MasterLiveDataMode As New BoolWithEventStatus()

    End Class

    Public Class BoolWithEventStatus

        Public Event Status As EventHandler
        Protected Overridable Sub StatusChange()
            RaiseEvent Status(Me, EventArgs.Empty)
        End Sub
        Private _State As Boolean = True
        Public Property State() As Boolean
            Get
                Return _State
            End Get
            Set
                _State = Value
                StatusChange()
            End Set
        End Property
    End Class

End Namespace
''' <summary>
''' These are the LEGAL classes that AB will accept as a DataType.
''' This should be used in writing and data.
''' This format will be returned with the Variable Names.
''' </summary>
Public Enum ABTagClass

    BOOL = 0
    SINT = 1
    INT = 2
    DINT = 3
    REAL = 4
    [STRING] = 5
    LINT = 6
    [OBJECT] = 7
    NotFound = 99

End Enum

Public Enum ReturnResult
    Success
    SuccessWithWarnings
    Failed
    FailedWithErrors
    FileNotFound
    FileLoadError
    FileCorupt

End Enum
