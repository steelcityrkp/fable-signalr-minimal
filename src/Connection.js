
let apiBaseUrl = 'http://localhost:7071';
let connected = false;

const connection = new signalR.HubConnectionBuilder()
.withUrl(`${apiBaseUrl}/api`)
.configureLogging(signalR.LogLevel.Information)
.build();

function initConnection(){
    console.log('registering handlers...');
    
    connection.onclose(()=> console.log('disconnected'));
    console.log('connecting...');
    connection.start()
      .then(() => connected = true)
      .catch(console.error);
}

export {connected, initConnection}