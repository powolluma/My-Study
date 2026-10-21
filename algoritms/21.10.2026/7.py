def remove_duplicates(s):
    stack = []

    for ch in s:
        if stack and stack[-1] == ch:
            stack.pop()
        else:
            stack.append(ch)

    return ''.join(stack)


print(remove_duplicates("aabb"))
print(remove_duplicates("abbaca"))