import MiddleFooter from "./MiddleFooter"
import TopFooter from "./TopFooter"

const Footer = () => {
  return (
    <div>
      <TopFooter/>
      <MiddleFooter/>
      <p className="text-gray text-center my-8 py-4">Copyright © 2024 Pharmacy. All Rights Reserved.</p>
    </div>
  )
}

export default Footer