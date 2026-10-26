import asyncio


class Timer:
    def __init__(self, seconds):
        self.seconds = seconds
        self.running = asyncio.Event()
        self.running.set()

        self.handlers = {
            "tick": [],
            "finish": [],
            "pause": [],
            "resume": []
        }

    def on(self, event, handler):
        self.handlers[event].append(handler)

    def emit(self, event, *args):
        for handler in self.handlers[event]:
            handler(*args)

    def pause(self):
        self.running.clear()
        self.emit("pause")

    def resume(self):
        self.running.set()
        self.emit("resume")

    async def start(self):
        while self.seconds > 0:
            for i in range(10):
                await self.running.wait()
                await asyncio.sleep(0.1)

            self.seconds -= 1
            self.emit("tick", self.seconds)

        self.emit("finish")


async def main():
    timer = Timer(5)

    timer.on("tick", lambda s: print("Осталось:", s))
    timer.on("finish", lambda: print("Время вышло"))
    timer.on("pause", lambda: print("Пауза"))
    timer.on("resume", lambda: print("Продолжение"))

    task = asyncio.create_task(timer.start())

    await asyncio.sleep(2)
    timer.pause()

    await asyncio.sleep(2)
    timer.resume()

    await task


asyncio.run(main())