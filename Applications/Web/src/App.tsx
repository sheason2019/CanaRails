import { appApi } from "./api";

function App() {
  const handleClick = async () => {
    const resp = await appApi.createApp({});
    console.log("resp", resp);
  };

  return (
    <>
      <p>Hello world</p>
      <button onClick={handleClick}>Create App</button>
    </>
  );
}

export default App;
