import { Spin } from 'antd';
import Layout, { Header, Content, Footer } from 'antd/lib/layout/layout';
import { useState } from 'react';
import { useRef } from 'react';
import { useEffect } from 'react';
import ReactMarkdown from 'react-markdown'
import './App.css';
import logo from './logo.svg';

function App() {
  const [ready, setReady] = useState(false);
  
  let md = useRef("# hello");

  useEffect(() => {
    fetch('markdown/index.md').then((r) => r.text()).then(text => {
      md.current = text;
      console.log("xd"+text);
      setReady(true)
    })
  }, [ready])

  return ready ? (
    <div className="App">
      <Layout>
        <Header>
        <img alt="fluint-logo" className="Logo" src={logo}/>
        <div className="RepositoryContainer">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" className="RepositoryIcon">
            <path d="M439.55 236.05L244 40.45a28.87 28.87 0 00-40.81 0l-40.66 40.63 51.52 51.52c27.06-9.14 52.68 16.77 43.39 43.68l49.66 49.66c34.23-11.8 61.18 31 35.47 56.69-26.49 26.49-70.21-2.87-56-37.34L240.22 199v121.85c25.3 12.54 22.26 41.85 9.08 55a34.34 34.34 0 01-48.55 0c-17.57-17.6-11.07-46.91 11.25-56v-123c-20.8-8.51-24.6-30.74-18.64-45L142.57 101 8.45 235.14a28.86 28.86 0 000 40.81l195.61 195.6a28.86 28.86 0 0040.8 0l194.69-194.69a28.86 28.86 0 000-40.81z"></path>
          </svg>
        </div>
        </Header>
        <Layout style={{alignContent: "center", content: "flex"}}>
          <Content className="Site-layout">
            <div className="Site-layout-background"  style={{alignContent: "start", content: "flex"}}>
              <div style={{paddingTop: 20, paddingLeft: 20, paddingRight: 20}}>
                <ReactMarkdown>{md.current}</ReactMarkdown>
              </div>
            </div>
          </Content>
        </Layout>
        <Footer style={{textAlign:"center"}}>All Rights Reserved Rainboi64(C)</Footer>
      </Layout>
    </div>
  ) : <Spin className="Centered" size="large"/> ;
}

export default App;
