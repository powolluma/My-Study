import unittest

class TestExample(unittest.TestCase):

    def test_sum(self):
        self.assertEqual(2 + 3, 5)


if __name__ == "__main__":
    unittest.main()