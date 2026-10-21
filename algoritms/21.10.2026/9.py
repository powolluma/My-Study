class Node:
    def __init__(self, value):
        self.value = value
        self.left = None
        self.right = None


def preorder_iter(root):
    result = []
    stack = [root] if root else []

    while stack:
        node = stack.pop()
        result.append(node.value)

        if node.right:
            stack.append(node.right)
        if node.left:
            stack.append(node.left)

    return result


def inorder_iter(root):
    result = []
    stack = []
    node = root

    while node or stack:
        while node:
            stack.append(node)
            node = node.left

        node = stack.pop()
        result.append(node.value)
        node = node.right

    return result


def postorder_iter(root):
    result = []
    stack = [(root, False)] if root else []

    while stack:
        node, visited = stack.pop()

        if visited:
            result.append(node.value)
        else:
            stack.append((node, True))

            if node.right:
                stack.append((node.right, False))
            if node.left:
                stack.append((node.left, False))

    return result


def preorder_rec(node):
    if not node:
        return []
    return [node.value] + preorder_rec(node.left) + preorder_rec(node.right)


def inorder_rec(node):
    if not node:
        return []
    return inorder_rec(node.left) + [node.value] + inorder_rec(node.right)


def postorder_rec(node):
    if not node:
        return []
    return postorder_rec(node.left) + postorder_rec(node.right) + [node.value]


root = Node(1)
root.left = Node(2)
root.right = Node(3)
root.left.left = Node(4)
root.left.right = Node(5)

print("Preorder:", preorder_iter(root), preorder_rec(root))
print("Inorder:", inorder_iter(root), inorder_rec(root))
print("Postorder:", postorder_iter(root), postorder_rec(root))