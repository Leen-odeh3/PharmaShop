import googlePlay from "/Images/google-play.png";
import appStore from "/Images/app-store.png";

const TopHeader = () => {
  return (
    <div
      className=" grid grid-cols-1 sm:grid-cols-3 gap-4 rounded-100 p-2 m-2 my-3 "
      style={{
        borderTop: ".2px solid #f0eeeb",
        borderBottom: ".2px solid #f0eeeb",
        marginTop:"50px"
      }}
    >
      <div
        className="p-4"
        style={{
          borderRight: ".2px solid #f0eeeb",
        }}
      >
        <div className="flex justify-center align-middle  items-center">
          <i className="bi bi-globe-americas pr-4 text-5xl text-primary align-middle"></i>
          <div style={{ textAlign: "start" }}>
            <h2 className="font-bold py-1">Address</h2>
            <p className="text-gray">
              9066 Green Lake Drive
              <br /> Chevy Chase, MD 20815
            </p>
          </div>
        </div>
      </div>

      {/*  */}

      <div
        className="p-4"
        style={{
          borderRight: ".2px solid #f0eeeb",
        }}
      >
        <div className="flex justify-center align-middle items-center">
          <i className="bi bi-whatsapp pr-4 text-5xl text-primary align-middle"></i>
          <div style={{ textAlign: "start" }}>
            <h2 className="font-bold py-1">Whatsapp Us</h2>
            <p className="text-gray">(1800)-88-66-990</p>
            <p className="text-gray">contact@example.com</p>
          </div>
        </div>
      </div>

      <div className="p-4 ">
        <p className="font-bold text-center py-3 text-xl">
          Download the app now!
        </p>
        <div className="flex justify-center align-middle">
          <img src={googlePlay} alt="googleplay" />
          <img src={appStore} alt="appstore" className="pl-2" />
        </div>
      </div>
    </div>
  );
};

export default TopHeader;