# 🛒 Cartify



Cartify is a modern E-Commerce web application built with **ASP.NET Core MVC**, **Entity Framework Core**, and **Identity** for authentication & authorization.  
It provides a full shopping experience with product management, cart handling, and customer feedback system.

---
## 🎥 Demo

<p align="center">
  <a href="https://www.youtube.com/watch?v=lbT01bu-pvY" target="_blank">
    <img src="vid.png" 
         alt="Watch the demo" width="720"/>
  </a>
</p>




## 🚀 Features

- **Authentication & Authorization**  
  Secure login/register system using **Identity** with role-based access (Admin / Customer).

- **Admin Dashboard**  
  - Add, edit, and delete products and categories.  
  - Manage product/category images (storing the URL in database & storing the images in `wwwroot`).  
  - Dashboard to display all products and categories.

- **Product Management (CRUD)**  
  Full Create, Read, Update, Delete operations for products.

- **Cart & Checkout**  
  - Add products to cart.  
  - State management for unauthenticated users using cookies.  

- **User Feedback**  
  - Add comments on each product page to share reviews.  
  - Display users’ feedback under each product.  
  - Receive general shopping experience reviews from users.

- **Filtering & Pagination**  
  - Filter products by category or keywords.
  - Search products using a server-side search bar for faster, scalable results.
  - Pagination with **jQuery DataTable** for smooth navigation.  
  - Ajax calls to dynamically load filter names per category.

- **Repository Pattern + Dependency Injection**  
  Clean architecture with reusable, testable code.
------
## 🛠️ Tech Stack

- **ASP.NET Core MVC**  
- **Entity Framework Core (Code First)**  
- **Identity for Authentication & Authorization**  
- **Repository Pattern & Dependency Injection**
- - **Front-End**  
  - Based on a free **Bootstrap template**.  [Src](https://bootstrapmade.com/techie-free-skin-bootstrap-3/)
  - Additional pages built from scratch and some template edits for customization.
- **Bootstrap (Frontend)**  
- **jQuery + Ajax**  
