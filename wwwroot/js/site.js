(() => {
  const root = document.documentElement;
  const themeKey = 'ms-theme';
  const accentKey = 'ms-accent';
  const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

  const storedTheme = localStorage.getItem(themeKey);
  if (storedTheme === 'light' || storedTheme === 'dark') {
    root.setAttribute('data-theme', storedTheme);
  } else {
    root.setAttribute('data-theme', 'system');
  }

  const storedAccent = localStorage.getItem(accentKey);
  if (storedAccent) {
    root.setAttribute('data-accent', storedAccent);
  }

  const themeButton = document.querySelector('[data-theme-toggle]');
  themeButton?.addEventListener('click', () => {
    const current = root.getAttribute('data-theme');
    const next = current === 'dark' ? 'light' : 'dark';
    root.setAttribute('data-theme', next);
    localStorage.setItem(themeKey, next);
  });

  document.querySelectorAll('[data-accent-option]').forEach((button) => {
    button.addEventListener('click', () => {
      const accent = button.getAttribute('data-accent-option');
      if (!accent) return;
      root.setAttribute('data-accent', accent);
      localStorage.setItem(accentKey, accent);
    });
  });

  if (prefersReducedMotion) {
    document.querySelectorAll('.section-reveal').forEach((item) => item.classList.add('is-visible'));
    return;
  }

  const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add('is-visible');
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.15, rootMargin: '0px 0px -24px 0px' });

  document.querySelectorAll('.section-reveal').forEach((item) => observer.observe(item));
})();
