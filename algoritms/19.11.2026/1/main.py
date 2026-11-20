import random
import pygame


WIDTH = 600
HEIGHT = 600


class Player:
    def __init__(self):
        self.rect = pygame.Rect(WIDTH // 2 - 25, HEIGHT - 70, 50, 50)
        self.speed = 5
        self.health = 3

    def update(self, keys):
        if keys[pygame.K_LEFT]:
            self.rect.x -= self.speed
        if keys[pygame.K_RIGHT]:
            self.rect.x += self.speed
        if keys[pygame.K_UP]:
            self.rect.y -= self.speed
        if keys[pygame.K_DOWN]:
            self.rect.y += self.speed

        self.rect.clamp_ip(pygame.Rect(0, 0, WIDTH, HEIGHT))

    def draw(self, screen):
        pygame.draw.rect(screen, (50, 150, 255), self.rect)


class Enemy:
    def __init__(self):
        x = random.randint(0, WIDTH - 40)
        self.rect = pygame.Rect(x, -40, 40, 40)
        self.speed = random.randint(3, 6)

    def update(self):
        self.rect.y += self.speed

    def draw(self, screen):
        pygame.draw.rect(screen, (220, 60, 60), self.rect)


class Game:
    def __init__(self):
        pygame.init()
        self.screen = pygame.display.set_mode((WIDTH, HEIGHT))
        pygame.display.set_caption("Игра")
        self.clock = pygame.time.Clock()
        self.font = pygame.font.SysFont(None, 32)
        self.big_font = pygame.font.SysFont(None, 52)

        self.player = Player()
        self.enemies = []
        self.spawn_timer = 0
        self.start_time = pygame.time.get_ticks()
        self.end_time = None
        self.running = True
        self.game_over = False

    def run(self):
        while self.running:
            dt = self.clock.tick(60) / 1000

            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    self.running = False
                if event.type == pygame.KEYDOWN and event.key == pygame.K_ESCAPE:
                    self.running = False

            if not self.game_over:
                self.update(dt)

            self.draw()

        pygame.quit()

    def update(self, dt):
        keys = pygame.key.get_pressed()
        self.player.update(keys)

        self.spawn_timer += dt
        if self.spawn_timer >= 0.8:
            self.enemies.append(Enemy())
            self.spawn_timer = 0

        for enemy in self.enemies[:]:
            enemy.update()

            if enemy.rect.colliderect(self.player.rect):
                self.player.health -= 1
                self.enemies.remove(enemy)
            elif enemy.rect.top > HEIGHT:
                self.enemies.remove(enemy)

        if self.player.health <= 0:
            self.game_over = True
            self.end_time = pygame.time.get_ticks()

    def draw(self):
        self.screen.fill((245, 245, 245))

        self.player.draw(self.screen)
        for enemy in self.enemies:
            enemy.draw(self.screen)

        current_time = self.end_time if self.game_over else pygame.time.get_ticks()
        survival_time = (current_time - self.start_time) // 1000

        health_text = self.font.render(
            f"Здоровье: {self.player.health}", True, (20, 20, 20)
        )
        time_text = self.font.render(
            f"Время: {survival_time} сек.", True, (20, 20, 20)
        )
        self.screen.blit(health_text, (10, 10))
        self.screen.blit(time_text, (10, 45))

        if self.game_over:
            text = self.big_font.render("ИГРА ОКОНЧЕНА", True, (180, 30, 30))
            hint = self.font.render("Нажмите Esc для выхода", True, (20, 20, 20))
            self.screen.blit(text, text.get_rect(center=(WIDTH // 2, HEIGHT // 2)))
            self.screen.blit(hint, hint.get_rect(center=(WIDTH // 2, HEIGHT // 2 + 45)))

        pygame.display.flip()


def main():
    game = Game()
    game.run()


if __name__ == "__main__":
    main()
