import unittest
from shapes import Point, Triangle


class TestTriangle(unittest.TestCase):
    def test_area(self):
        triangle = Triangle(Point(0, 0), Point(4, 0), Point(0, 3))
        self.assertEqual(triangle.area(), 6.0)


if __name__ == "__main__":
    unittest.main()
