(() => {
  const root = document.documentElement;
  const themeKey = 'ms-theme';
  const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

  const storedTheme = localStorage.getItem(themeKey);
  if (storedTheme === 'light' || storedTheme === 'dark') {
    root.setAttribute('data-theme', storedTheme);
  } else {
    root.setAttribute('data-theme', 'system');
  }

  const themeButton = document.querySelector('[data-theme-toggle]');
  themeButton?.addEventListener('click', () => {
    const current = root.getAttribute('data-theme');
    const next = current === 'dark' ? 'light' : 'dark';
    root.setAttribute('data-theme', next);
    localStorage.setItem(themeKey, next);
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
