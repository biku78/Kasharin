
import asyncio
import websockets

async def hello():
    uri = "ws://localhost:25564"
    async with websockets.connect(uri) as websocket:
        await websocket.send("Hello world!")

asyncio.get_event_loop().run_until_complete(hello())