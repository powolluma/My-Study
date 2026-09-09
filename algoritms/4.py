start_second = int(input("Введите кол-во секунд: ")) # 3700

hours = start_second // 3600 # 1, Остаток 100 сек
minute = (start_second % 3600) // 60 # 1, остаток 30 сек
end_second = start_second % 60

print(f"{hours} часов, {minute} минут, {end_second} секунд")
