
# 📚 EF Performances

A project to explore and compare data loading strategies in **Entity Framework Core**: **Eager Loading**, **Lazy Loading**, and **Explicit Loading**. Perfect for learning and optimizing your queries!

---

## 🚀 Features

- 📖 **Manage Books, Authors, and Categories**:
  - Relationship: A book belongs to an author and a category.
- 🛠️ **Loading Strategies**:
  - **Eager Loading**: Loads all related data in a single query.
  - **Lazy Loading**: Loads related data only when accessed.
  - **Explicit Loading**: Manually loads related data.
- 📊 **Performance Benchmarks**:
  - Execution time for each strategy.
  - Generated SQL queries.

---

## 🛠️ Technologies Used

- **.NET 6**
- **Entity Framework Core**
- **SQLite**

---

## 📁 Data Model

```plaintext
Author (1) ---- (N) Book (N) ---- (1) Category
```

### Tables:
- **Author**: `Id`, `FirstName`, `Surname`
- **Book**: `Id`, `Title`, `YearPublished`, `AuthorId`, `CategoryId`
- **Category**: `Id`, `Name`

---

## 🌟 Loading Strategies

### 1️⃣ **Eager Loading**
- **Description**: Loads all related data upfront.
- **Generated SQL Query**:
  ```sql
  SELECT "b"."Id", "b"."Title", "b"."YearPublished", "b"."AuthorId", 
         "b"."CategoryId", "a"."Id", "a"."FirstName", "a"."Surname", 
         "c"."Id", "c"."Name"
  FROM "Books" AS "b"
  INNER JOIN "Authors" AS "a" ON "b"."AuthorId" = "a"."Id"
  INNER JOIN "Categories" AS "c" ON "b"."CategoryId" = "c"."Id";
  ```
- **Code Example**:
  ```csharp
  var books = _context.Books
      .Include(b => b.Author)
      .Include(b => b.Category)
      .ToList();
  ```

---

### 2️⃣ **Lazy Loading**
- **Description**: Loads related data only when accessed.
- **Generated SQL Query** (per access):
  ```sql
  SELECT * FROM "Authors" WHERE "Id" = @p0;
  SELECT * FROM "Categories" WHERE "Id" = @p0;
  ```
- **Code Example**:
  ```csharp
  foreach (var book in _context.Books)
  {
      var author = book.Author; // Loads on access
      var category = book.Category; // Loads on access
  }
  ```

---

### 3️⃣ **Explicit Loading**
- **Description**: Manually load related data as needed.
- **Generated SQL Query**:
  ```sql
  SELECT * FROM "Authors" WHERE "Id" = @p0;
  SELECT * FROM "Categories" WHERE "Id" = @p0;
  ```
- **Code Example**:
  ```csharp
  foreach (var book in _context.Books)
  {
      _context.Entry(book).Reference(b => b.Author).Load();
      _context.Entry(book).Reference(b => b.Category).Load();
  }
  ```

---

## 📊 Performance Benchmarks

| Strategy           | Generated Queries   | Time (ms)    | Advantages                               | Disadvantages                        |
|--------------------|---------------------|--------------|------------------------------------------|---------------------------------------|
| **Eager Loading**  | 1                   | Fast         | Fewer database roundtrips                | May load unnecessary data             |
| **Lazy Loading**   | N+1 (per relation)  | Slower       | Loads only accessed data                 | More SQL queries                      |
| **Explicit Loading** | N+1 (controlled)  | Moderate     | Full control over loaded relationships   | Requires more manual code             |

---

## 🔧 How to Run

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/library-insights.git
   cd library-insights
   ```
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Apply migrations and run:
   ```bash
   dotnet ef database update
   dotnet run
   ```


## 🤝 Contributions

Contributions are welcome! Open an **issue** or submit a **pull request**.
