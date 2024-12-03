import { BrowserRouter, Route, Routes } from "react-router-dom";
import { BarChart, LineChart, PieChart, Brand, Category , Order, Transaction,MainDash, About, Blog, Seals, Shop, Contact, Product, Home, NotFound, User, Login, Profile, ForgotPass, Register } from "./path"; 
import Layout from "./Layout";

const Index = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout children={undefined}/>}> 
          <Route index element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/blog" element={<Blog />} />
          <Route path="/onsales" element={<Seals />} />
          <Route path="/shop" element={<Shop />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/product" element={<Product />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/forgot-pass" element={<ForgotPass />} />
          <Route path="/profile" element={<Profile />} />
        </Route>

        <Route path="/dashboard/admin" element={<MainDash />} />
        <Route path="/dashboard/admin/transaction" element={<Transaction/>} />
        <Route path="/dashboard/admin/brand" element={<Brand/>} />
        <Route path="/dashboard/admin/product" element={<Product/>} />
        <Route path="/dashboard/admin/category" element={<Category/>} />
        <Route path="/dashboard/admin/orders" element={<Order/>} />
        <Route path="/dashboard/admin/customer" element={<User/>} />

         <Route path="/dashboard/admin/chart/bar" element={<BarChart />} />
          <Route path="/dashboard/admin/chart/pie" element={<PieChart />} />
          <Route path="/dashboard/admin/chart/line" element={<LineChart />} />


        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
};

export default Index;
