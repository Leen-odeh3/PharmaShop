import { useEffect, useState } from "react";
import Routing from "./Routing/index";
import { DotLoader } from "react-spinners";

const App = () => {
  const [loading, setLoading] = useState<boolean>(true); 

  useEffect(() => {
    const timer = setTimeout(() => {
      setLoading(false);
    }, 1200);

    return () => clearTimeout(timer);
  }, []);

  return (
    <>
      {loading ? (
        <div style={styles.loaderContainer}>
          <DotLoader color="#24AEB1" size={100} loading={loading} />
        </div>
      ) : (
        <Routing />
      )}
    </>
  );
};

const styles = {
  loaderContainer: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    height: "100vh", 
  },
};

export default App;