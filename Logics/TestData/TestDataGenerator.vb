Imports System.Collections.Generic

''' <summary>
''' テスト用データ生成ロジック
''' 開発・テスト時のみ使用
''' </summary>
Public Class TestDataGenerator

    ''' <summary>
    ''' テスト用のサンプルデータを生成する
    ''' 構造: Dictionary(Of スキーマ名, Dictionary(Of テーブル名, List(Of 行データ)))
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GenerateTestData() As Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))
        Dim allData As New Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))()

        ' SCHEMA1 のデータを生成
        Dim schema1 As New Dictionary(Of String, List(Of Dictionary(Of String, Object)))()
        schema1.Add("EMP_TABLE", GenerateEmployeeData())
        schema1.Add("DEPT_TABLE", GenerateDepartmentData())
        schema1.Add("SALARY_TABLE", GenerateSalaryData())
        allData.Add("SCHEMA1", schema1)

        ' SCHEMA2 のデータを生成
        Dim schema2 As New Dictionary(Of String, List(Of Dictionary(Of String, Object)))()
        schema2.Add("USERS_TABLE", GenerateUsersData())
        schema2.Add("PRODUCTS_TABLE", GenerateProductsData())
        schema2.Add("ORDERS_TABLE", GenerateOrdersData())
        allData.Add("SCHEMA2", schema2)

        Return allData
    End Function

    ''' <summary>
    ''' 従業員テーブルのサンプルデータを生成
    ''' </summary>
    Private Shared Function GenerateEmployeeData() As List(Of Dictionary(Of String, Object))
        Dim employees As New List(Of Dictionary(Of String, Object))()
        Dim empData As String() = {
            "1,太郎,営業部,500000",
            "2,花子,営業部,480000",
            "3,次郎,開発部,550000",
            "4,美咲,開発部,520000",
            "5,健太,企画部,490000",
            "6,由美,企画部,510000",
            "7,拓也,営業部,505000",
            "8,麗子,開発部,530000",
            "9,翔太,企画部,495000",
            "10,香織,営業部,485000"
        }

        For Each record In empData
            Dim fields = record.Split(",")
            Dim row As New Dictionary(Of String, Object)
            row.Add("EMP_ID", CInt(fields(0)))
            row.Add("EMP_NAME", fields(1))
            row.Add("DEPARTMENT", fields(2))
            row.Add("SALARY", CInt(fields(3)))
            employees.Add(row)
        Next

        Return employees
    End Function

    ''' <summary>
    ''' 部署テーブルのサンプルデータを生成
    ''' </summary>
    Private Shared Function GenerateDepartmentData() As List(Of Dictionary(Of String, Object))
        Dim departments As New List(Of Dictionary(Of String, Object))()
        Dim deptData As String() = {
            "1,営業部,東京",
            "2,開発部,大阪",
            "3,企画部,京都"
        }

        For Each record In deptData
            Dim fields = record.Split(",")
            Dim row As New Dictionary(Of String, Object)
            row.Add("DEPT_ID", CInt(fields(0)))
            row.Add("DEPT_NAME", fields(1))
            row.Add("LOCATION", fields(2))
            departments.Add(row)
        Next

        Return departments
    End Function

    ''' <summary>
    ''' 給与テーブルのサンプルデータを生成
    ''' </summary>
    Private Shared Function GenerateSalaryData() As List(Of Dictionary(Of String, Object))
        Dim salaries As New List(Of Dictionary(Of String, Object))()
        Dim salaryData As String() = {
            "1,2024-01-01,500000",
            "2,2024-01-01,480000",
            "3,2024-01-01,550000",
            "4,2024-01-01,520000",
            "5,2024-01-01,490000",
            "6,2024-01-01,510000",
            "7,2024-01-01,505000",
            "8,2024-01-01,530000",
            "9,2024-01-01,495000",
            "10,2024-01-01,485000"
        }

        For Each record In salaryData
            Dim fields = record.Split(",")
            Dim row As New Dictionary(Of String, Object)
            row.Add("EMP_ID", CInt(fields(0)))
            row.Add("SALARY_DATE", CDate(fields(1)))
            row.Add("AMOUNT", CInt(fields(2)))
            salaries.Add(row)
        Next

        Return salaries
    End Function

    ''' <summary>
    ''' ユーザーテーブルのサンプルデータを生成
    ''' </summary>
    Private Shared Function GenerateUsersData() As List(Of Dictionary(Of String, Object))
        Dim users As New List(Of Dictionary(Of String, Object))()
        Dim userData As String() = {
            "1,user@example.com,Taro Yamada,2024-01-15",
            "2,user2@example.com,Hanako Suzuki,2024-01-20",
            "3,user3@example.com,Jiro Tanaka,2024-02-01",
            "4,user4@example.com,Misaki Ito,2024-02-10",
            "5,user5@example.com,Kenta Yamamoto,2024-02-15"
        }

        For Each record In userData
            Dim fields = record.Split(",")
            Dim row As New Dictionary(Of String, Object)
            row.Add("USER_ID", CInt(fields(0)))
            row.Add("EMAIL", fields(1))
            row.Add("USER_NAME", fields(2))
            row.Add("CREATED_DATE", CDate(fields(3)))
            users.Add(row)
        Next

        Return users
    End Function

    ''' <summary>
    ''' 商品テーブルのサンプルデータを生成
    ''' </summary>
    Private Shared Function GenerateProductsData() As List(Of Dictionary(Of String, Object))
        Dim products As New List(Of Dictionary(Of String, Object))()
        Dim productData As String() = {
            "1001,ノートPC,150000,10",
            "1002,マウス,2000,50",
            "1003,キーボード,8000,30",
            "1004,モニター,35000,5",
            "1005,USBハブ,5000,20",
            "1006,外付けHDD,12000,15",
            "1007,Webカメラ,6000,25",
            "1008,スピーカー,4000,35"
        }

        For Each record In productData
            Dim fields = record.Split(",")
            Dim row As New Dictionary(Of String, Object)
            row.Add("PRODUCT_ID", CInt(fields(0)))
            row.Add("PRODUCT_NAME", fields(1))
            row.Add("PRICE", CInt(fields(2)))
            row.Add("STOCK", CInt(fields(3)))
            products.Add(row)
        Next

        Return products
    End Function

    ''' <summary>
    ''' 注文テーブルのサンプルデータを生成
    ''' </summary>
    Private Shared Function GenerateOrdersData() As List(Of Dictionary(Of String, Object))
        Dim orders As New List(Of Dictionary(Of String, Object))()
        Dim orderData As String() = {
            "1,1,1001,150000,2024-01-15",
            "2,2,1002,4000,2024-01-20",
            "3,3,1003,8000,2024-02-01",
            "4,1,1004,35000,2024-02-05",
            "5,4,1005,5000,2024-02-10",
            "6,2,1001,150000,2024-02-15",
            "7,5,1002,2000,2024-02-18",
            "8,3,1006,12000,2024-02-20"
        }

        For Each record In orderData
            Dim fields = record.Split(",")
            Dim row As New Dictionary(Of String, Object)
            row.Add("ORDER_ID", CInt(fields(0)))
            row.Add("USER_ID", CInt(fields(1)))
            row.Add("PRODUCT_ID", CInt(fields(2)))
            row.Add("AMOUNT", CInt(fields(3)))
            row.Add("ORDER_DATE", CDate(fields(4)))
            orders.Add(row)
        Next

        Return orders
    End Function

End Class
