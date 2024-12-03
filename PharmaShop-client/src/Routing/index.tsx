import { BrowserRouter, Route, Routes } from "react-router-dom";
import { About, Blog, Seals, Shop, Contact, Product, Home, Footer, Header, NotFound, User, Login, Profile, ForgotPass, Register } from "./path"; 

const index = () => {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route index element={<Home/>} />
        <Route path="/about" element={<About/>} />
        <Route path="/blog" element={<Blog />}/>
        <Route path="/onsales" element={<Seals/>} />
        <Route path="/shop" element={<Shop />}/>
        <Route path="/contact" element={<Contact />} />
        <Route path="/product" element={<Product />}/>
        <Route path="/user" element={<User />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register/>} />
        <Route path="/forgot-pass" element={<ForgotPass/>} />
        <Route path="/profile" element={<Profile />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
      <Footer />
    </BrowserRouter>
  );
};

export default index;